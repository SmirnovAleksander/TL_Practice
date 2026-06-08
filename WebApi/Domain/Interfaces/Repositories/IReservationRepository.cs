using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAll(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName );
    Task<Reservation?> GetById( Guid id );
    Task<Reservation> Create( Reservation reservation );
    Task Cancel( Guid id );
    Task<bool> HasOverlap( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate );
}
