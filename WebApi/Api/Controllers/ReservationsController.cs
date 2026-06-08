using Api.Dtos.Reservation;
using Api.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/reservations" )]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    public ReservationsController(
        IReservationRepository reservationRepository,
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository )
    {
        _reservationRepository = reservationRepository;
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? propertyId,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] string? guestName )
    {
        List<Reservation> reservations = await _reservationRepository
            .GetAll( propertyId, arrivalDate, departureDate, guestName );

        List<ReservationDto> reservationDtos = reservations
            .Select( r => r.ToReservationDto() )
            .ToList();

        return Ok( reservationDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id )
    {
        Reservation? reservation = await _reservationRepository.GetById( id );
        if ( reservation == null )
        {
            return NotFound();
        }

        return Ok( reservation.ToReservationDto() );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateReservationDto createDto )
    {
        Property? property = await _propertyRepository.GetById( createDto.PropertyId );
        if ( property == null )
        {
            return BadRequest( "Property not found" );
        }

        RoomType? roomType = await _roomTypeRepository.GetById( createDto.RoomTypeId );
        if ( roomType == null )
        {
            return BadRequest( "RoomType not found" );
        }
        if ( createDto.ArrivalDate >= createDto.DepartureDate )
        {
            return BadRequest( "ArrivalDate must be before DepartureDate" );
        }
        if ( createDto.Guests < roomType.MinPersonCount || createDto.Guests > roomType.MaxPersonCount )
        {
            return BadRequest( $"Guest must be between {roomType.MinPersonCount} and {roomType.MaxPersonCount}" );
        }

        bool hasOverlap = await _reservationRepository
            .HasOverlap( createDto.RoomTypeId, createDto.ArrivalDate, createDto.DepartureDate );

        if ( hasOverlap )
        {
            return BadRequest( "RoomType is not available for this date" );
        }

        Reservation reservation = createDto.ToReservationFromCreate();
        int nights = reservation.DepartureDate.DayNumber - reservation.ArrivalDate.DayNumber;
        reservation.Total = roomType.DailyPrice * nights;
        reservation.Currency = roomType.Currency;
        reservation.IsCanceled = false;
        Reservation created = await _reservationRepository.Create( reservation );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToReservationDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Cancel( [FromRoute] Guid id )
    {
        Reservation? reservation = await _reservationRepository.GetById( id );
        if ( reservation == null )
        {
            return NotFound();
        }

        await _reservationRepository.Cancel( id );

        return NoContent();
    }

}
