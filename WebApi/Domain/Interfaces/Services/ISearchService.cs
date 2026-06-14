using Domain.Dtos.Reservation;
using Domain.Dtos.Search;

namespace Domain.Interfaces.Services;

public interface ISearchService
{
    Task<IReadOnlyList<SearchResultServiceDto>> SearchAsync( SearchFilterServiceDto filter, CancellationToken ct = default );
}
