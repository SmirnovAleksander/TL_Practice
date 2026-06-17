using Infrastructure.Dto.Reservations;
using Domain.Entities;

namespace Infrastructure.Interfaces.Services;

public interface IReservationService
{
    Task<IReadOnlyList<Reservation>> GetAllAsync( ReservationFilterDto filter, CancellationToken ct );

    Task<Reservation> GetByIdAsync( Guid id, CancellationToken ct );

    Task<Reservation> CreateAsync( CreateReservationDto dto, CancellationToken ct );

    Task CancelAsync( Guid id, CancellationToken ct );
}
