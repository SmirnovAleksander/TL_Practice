using Domain.Dtos.Reservation;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Infrastructure.Foundation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public ReservationRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Reservation>> GetAll(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        CancellationToken ct = default )
    {
        ReservationFilterServiceDto filter = new ReservationFilterServiceDto
        {
            PropertyId = propertyId,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            GuestName = guestName
        };

        IQueryable<Reservation> query = _dbContext.Reservations.AsQueryable().ReservationFilter( filter );

        return await query.ToListAsync( ct );
    }

    public async Task<Reservation?> GetById( Guid id, CancellationToken ct = default )
    {
        return await _dbContext.Reservations.FindAsync( id, ct );
    }

    public async Task<Reservation> Create( Reservation reservation, CancellationToken ct = default )
    {
        await _dbContext.Reservations.AddAsync( reservation, ct );
        await _dbContext.SaveChangesAsync( ct );

        return reservation;
    }

    public async Task Cancel( Guid id, CancellationToken ct = default )
    {
        Reservation? reservation = await _dbContext.Reservations.FindAsync( id, ct );
        if ( reservation == null )
        {
            throw new InvalidOperationException( $"Reservation with id '{id}' not found" );
        }

        reservation.IsCanceled = true;

        await _dbContext.SaveChangesAsync( ct );
    }

    public async Task<bool> HasOverlap(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        CancellationToken ct = default )
    {
        return await _dbContext.Reservations.AnyAsync( r =>
            r.RoomTypeId == roomTypeId &&
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate, ct );
    }
}
