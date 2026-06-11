using Api.Dtos.RoomType;
using Api.Mappers.Entity;
using Api.Mappers.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api" )]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypesController( IRoomTypeService roomTypeService )
    {
        _roomTypeService = roomTypeService;
    }

    [HttpGet( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> GetByProperty( [FromRoute] Guid propertyId, CancellationToken ct )
    {
        List<RoomType> roomTypes = await _roomTypeService.GetByPropertyAsync( propertyId, ct );
        List<RoomTypeDto> roomTypeDtos = roomTypes.Select( r => r.ToRoomTypeDto() ).ToList();

        return Ok( roomTypeDtos );
    }

    [HttpGet( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id, CancellationToken ct )
    {
        RoomType roomType = await _roomTypeService.GetByIdAsync( id, ct );

        return Ok( roomType.ToRoomTypeDto() );
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> Create( [FromRoute] Guid propertyId, [FromBody] CreateRoomTypeDto createDto, CancellationToken ct )
    {
        RoomType created = await _roomTypeService.CreateAsync( createDto.ToServiceDto( propertyId ), ct );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToRoomTypeDto() );
    }

    [HttpPut( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdateRoomTypeDto updateDto, CancellationToken ct )
    {
        RoomType updated = await _roomTypeService.UpdateAsync( updateDto.ToServiceDto( id ), ct );

        return Ok( updated.ToRoomTypeDto() );
    }

    [HttpDelete( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id, CancellationToken ct )
    {
        await _roomTypeService.DeleteAsync( id, ct );

        return NoContent();
    }
}
