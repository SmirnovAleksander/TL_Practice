using Api.Dto.Reservation;
using Api.Mappers.Entity;
using Api.Mappers.Service;
using Infrastructure.Dto.Reservation;
using Domain.Entities;
using Infrastructure.Interfaces.Services;
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
        ReservationFilterDto filter = new ReservationFilterDto
        {
            PropertyId = propertyId,
            ArrivalDate = arrivalDate,
            DepartureDate = departureDate,
            GuestName = guestName
        };

        IReadOnlyList<Reservation> reservations = await _reservationService.GetAllAsync( filter, ct );
        List<ReservationResponse> reservationDtos = reservations.Select( r => r.ToReservationDto() ).ToList();

        return Ok( reservationDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id, CancellationToken ct )
    {
        Reservation reservation = await _reservationService.GetByIdAsync( id, ct );

        return Ok( reservation.ToReservationDto() );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateReservationRequest request, CancellationToken ct )
    {
        Reservation created = await _reservationService.CreateAsync( request.ToDto(), ct );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToReservationDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Cancel( [FromRoute] Guid id, CancellationToken ct )
    {
        await _reservationService.CancelAsync( id, ct );

        return NoContent();
    }

}
