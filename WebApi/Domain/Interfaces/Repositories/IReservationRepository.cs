using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<IReadOnlyList<Reservation>> GetAllAsync(
        Guid? propertyId,
        DateOnly? arrivalDate,
        DateOnly? departureDate,
        string? guestName,
        CancellationToken ct );

    Task<Reservation?> GetByIdAsync( Guid id, CancellationToken ct );

    Task<Reservation> CreateAsync( Reservation reservation, CancellationToken ct );

    Task<Reservation> UpdateAsync( Reservation reservation, CancellationToken ct );

    Task<bool> HasOverlapAsync(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        CancellationToken ct );
}
