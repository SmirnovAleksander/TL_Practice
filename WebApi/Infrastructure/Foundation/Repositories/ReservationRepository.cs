using Infrastructure.Dto.Reservation;
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

    public async Task<IReadOnlyList<Reservation>> GetAllAsync(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        CancellationToken ct )
    {
        ReservationFilterDto filter = new ReservationFilterDto
        {
            PropertyId = propertyId,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            GuestName = guestName
        };

        return await _dbContext.Reservations
            .ApplyFilter( filter )
            .ToListAsync( ct );
    }

    public async Task<Reservation?> GetByIdAsync( Guid id, CancellationToken ct )
    {
        return await _dbContext.Reservations.FirstOrDefaultAsync( r => r.Id == id, ct );
    }

    public async Task<Reservation> CreateAsync( Reservation reservation, CancellationToken ct )
    {
        await _dbContext.Reservations.AddAsync( reservation, ct );
        await _dbContext.SaveChangesAsync( ct );

        return reservation;
    }

    public async Task<Reservation> UpdateAsync( Reservation reservation, CancellationToken ct )
    {
        _dbContext.Reservations.Update( reservation );
        await _dbContext.SaveChangesAsync( ct );

        return reservation;
    }

    public async Task<bool> HasOverlapAsync(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        CancellationToken ct )
    {
        return await _dbContext.Reservations.AnyAsync( r =>
            r.RoomTypeId == roomTypeId &&
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate, ct );
    }
}
