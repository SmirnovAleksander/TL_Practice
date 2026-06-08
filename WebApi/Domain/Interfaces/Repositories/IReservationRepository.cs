using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    List<Reservation> GetAll(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName );
    Reservation? GetById( Guid id );
    Reservation Create( Reservation reservation );
    void Cancel( Guid id );
    bool HasOverlap( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate );
}
