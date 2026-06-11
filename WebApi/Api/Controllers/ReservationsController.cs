using Api.Dtos.Reservation;
using Api.Mappers.Entity;
using Api.Mappers.Service;
using Domain.Dtos.Reservation;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/reservations" )]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? propertyId,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] string? guestName,
         CancellationToken ct )
    {
        ReservationFilterServiceDto filter = new ReservationFilterServiceDto
        {
            PropertyId = propertyId,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            GuestName = guestName
        };

        List<Reservation> reservations = await _reservationService.GetAllAsync( filter, ct );
        List<ReservationDto> reservationDtos = reservations.Select( r => r.ToReservationDto() ).ToList();

        return Ok( reservationDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id, CancellationToken ct )
    {
        Reservation reservation = await _reservationService.GetByIdAsync( id, ct );

        return Ok( reservation.ToReservationDto() );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateReservationDto createDto, CancellationToken ct )
    {
        Reservation created = await _reservationService.CreateAsync( createDto.ToServiceDto(), ct );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToReservationDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Cancel( [FromRoute] Guid id, CancellationToken ct )
    {
        await _reservationService.CancelAsync( id, ct );

        return NoContent();
    }

}
