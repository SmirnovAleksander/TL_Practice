using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<IReadOnlyList<Reservation>> GetAllAsync(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        CancellationToken ct = default );
    Task<Reservation?> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<Reservation> CreateAsync( Reservation reservation, CancellationToken ct = default );
    Task<Reservation> UpdateAsync( Reservation reservation, CancellationToken ct = default );
    Task<bool> HasOverlapAsync(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        CancellationToken ct = default );
}
