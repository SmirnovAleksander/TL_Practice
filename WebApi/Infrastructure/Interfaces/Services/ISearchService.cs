using Infrastructure.Dto.Reservations;
using Infrastructure.Dto.Searches;

namespace Infrastructure.Interfaces.Services;

public interface ISearchService
{
    Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct );
}
