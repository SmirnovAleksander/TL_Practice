using Infrastructure.Dto.Reservation;
using Infrastructure.Dto.Search;

namespace Infrastructure.Interfaces.Services;

public interface ISearchService
{
    Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct );
}
