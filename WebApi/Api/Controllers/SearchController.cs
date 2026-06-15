using Api.Dto.Reservation;
using Api.Mappers.Entity;
using Infrastructure.Dto.Reservation;
using Infrastructure.Dto.Search;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/search" )]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController( ISearchService searchService )
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] string? city,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] int? guests,
        [FromQuery] decimal? maxPrice,
        CancellationToken ct )
    {
        SearchFilterDto filter = new SearchFilterDto
        {
            City = city,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            Guests = guests,
            MaxPrice = maxPrice
        };

        IReadOnlyList<SearchResultDto> results = await _searchService.SearchAsync( filter, ct );
        List<SearchResultResponse> searchResults = results.Select( r => r.ToSearchResultDto() ).ToList();

        return Ok( searchResults );
    }
}
