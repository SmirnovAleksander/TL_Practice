using Api.Dtos.Reservation;
using Api.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/search" )]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;
    public SearchController(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    [HttpGet]
    public IActionResult Search(
        [FromQuery] string? city,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] int? guests,
        [FromQuery] decimal? maxPrice )
    {
        List<Property> allProperties = _propertyRepository.GetAll();
        if ( !string.IsNullOrWhiteSpace( city ) )
        {
            allProperties = allProperties
                .Where( p => p.City.Contains( city, StringComparison.OrdinalIgnoreCase ) )
                .ToList();
        }

        List<SearchResultDto> results = new List<SearchResultDto>();
        foreach ( Property property in allProperties )
        {
            List<RoomType> roomTypes = _roomTypeRepository.GetByProperty( property.Id );
            if ( guests.HasValue )
            {
                roomTypes = roomTypes
                    .Where( r => r.MinPersonCount <= guests && r.MaxPersonCount >= guests )
                    .ToList();
            }
            if ( maxPrice.HasValue )
            {
                roomTypes = roomTypes
                    .Where( r => r.DailyPrice <= maxPrice )
                    .ToList();
            }

            foreach ( RoomType roomType in roomTypes )
            {
                if ( arrivalDate.HasValue && departureDate.HasValue )
                {
                    bool hasOverlap = _reservationRepository
                        .HasOverlap( roomType.Id, arrivalDate.Value, departureDate.Value );
                    if ( hasOverlap )
                        continue;
                }
                results.Add( new SearchResultDto
                {
                    Property = property.ToPropertyDto(),
                    RoomType = roomType.ToRoomTypeDto(),
                    DailyPrice = roomType.DailyPrice,
                    Currency = roomType.Currency
                } );
            }
        }
        return Ok( results );
    }
}
