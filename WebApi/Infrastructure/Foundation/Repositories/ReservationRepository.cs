using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public ReservationRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<List<Reservation>> GetAll(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName )
    {
        IQueryable<Reservation> query = _dbContext.Reservations.AsQueryable();
        if ( propertyId.HasValue)
        {
            query = query.Where( r => r.PropertyId == propertyId );
        }
        if ( arrivalDate.HasValue)
        {
            query = query.Where( r => r.ArrivalDate >= arrivalDate );
        }
        if ( departureDate.HasValue)
        {
            query = query.Where( r => r.DepartureDate <= departureDate );
        }
        if ( !string.IsNullOrWhiteSpace( guestName ))
        {
            query = query.Where( r => r.GuestName.Contains( guestName ) );
        }

        return await query.ToListAsync();
    }

    public async Task<Reservation?> GetById( Guid id )
    {
        return await _dbContext.Reservations.FindAsync( id );
    }

    public async Task<Reservation> Create( Reservation reservation )
    {
        reservation.Id = Guid.NewGuid();
        _dbContext.Reservations.Add( reservation );
        await _dbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task Cancel( Guid id )
    {
        Reservation? reservation = await _dbContext.Reservations.FindAsync( id );
        if ( reservation == null )
        {
            return;
        }

        reservation.IsCanceled = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasOverlap( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        return await _dbContext.Reservations.AnyAsync( r =>
            r.RoomTypeId == roomTypeId &&
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate );
    }
}
