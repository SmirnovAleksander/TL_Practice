using Api.Dtos.Reservation;
using Api.Mappers.Entity;
using Domain.Dtos.Reservation;
using Domain.Dtos.Search;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/search" )]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public SearchController( IReservationService reservationService )
    {
        _reservationService = reservationService;
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
        SearchFilterServiceDto filter = new SearchFilterServiceDto
        {
            City = city,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            Guests = guests,
            MaxPrice = maxPrice
        };

        IReadOnlyList<SearchResultServiceDto> results = await _reservationService.SearchAsync( filter, ct );
        List<SearchResultDto> searchResults = results.Select( r => r.ToSearchResultDto() ).ToList();

        return Ok( results );
    }
}
