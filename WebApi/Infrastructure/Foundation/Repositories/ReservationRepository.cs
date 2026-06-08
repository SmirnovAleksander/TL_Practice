using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;

namespace Infrastructure.Foundation.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public ReservationRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public List<Reservation> GetAll(
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

        return query.ToList();
    }

    public Reservation? GetById( Guid id )
    {
        return _dbContext.Reservations.Find( id );
    }

    public Reservation Create( Reservation reservation )
    {
        reservation.Id = Guid.NewGuid();
        _dbContext.Reservations.Add( reservation );
        _dbContext.SaveChanges();

        return reservation;
    }

    public void Cancel( Guid id )
    {
        Reservation? reservation = _dbContext.Reservations.Find( id );
        if ( reservation == null )
        {
            return;
        }

        reservation.IsCanceled = true;

        _dbContext.SaveChanges();
    }

    public bool HasOverlap( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        return _dbContext.Reservations.Any( r =>
            r.RoomTypeId == roomTypeId &&
            !r.IsCanceled &&
            r.ArrivalDate < departureDate &&
            r.DepartureDate > arrivalDate );
    }
}
