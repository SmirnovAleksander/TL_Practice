using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAll(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        CancellationToken ct = default );
    Task<Reservation?> GetById( Guid id, CancellationToken ct = default );
    Task<Reservation> Create( Reservation reservation, CancellationToken ct = default );
    Task Cancel( Guid id, CancellationToken ct = default );
    Task<bool> HasOverlap(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        CancellationToken ct = default );
}
