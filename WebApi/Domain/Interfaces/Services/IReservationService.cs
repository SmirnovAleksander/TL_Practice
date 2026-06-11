using Domain.Dtos.Reservation;
using Domain.Dtos.Search;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IReservationService
{
    Task<List<Reservation>> GetAllAsync( ReservationFilterServiceDto filter, CancellationToken ct = default );
    Task<Reservation> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<Reservation> CreateAsync( CreateReservationServiceDto dto, CancellationToken ct = default );
    Task CancelAsync( Guid id, CancellationToken ct = default );
    Task<List<SearchResultServiceDto>> SearchAsync( SearchFilterServiceDto filter, CancellationToken ct = default );
}
