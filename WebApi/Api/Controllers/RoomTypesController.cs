using Api.Dtos.RoomType;
using Api.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api" )]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;
    public RoomTypesController( IRoomTypeRepository roomTypeRepository, IPropertyRepository propertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
    }

    [HttpGet( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> GetByProperty( [FromRoute] Guid propertyId )
    {
        Property? property = await _propertyRepository.GetById( propertyId );
        if ( property == null )
        {
            return NotFound();
        }

        List<RoomType> roomTypes = await _roomTypeRepository.GetByProperty( propertyId );
        List<RoomTypeDto> roomTypeDtos = roomTypes.Select( rt => rt.ToRoomTypeDto() ).ToList();

        return Ok( roomTypeDtos );
    }

    [HttpGet( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id )
    {
        RoomType? roomType = await _roomTypeRepository.GetById( id );
        if ( roomType == null )
        {
            return NotFound();
        }

        return Ok( roomType.ToRoomTypeDto() );
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> Create( [FromRoute] Guid propertyId, [FromBody] CreateRoomTypeDto createDto )
    {
        Property? property = await _propertyRepository.GetById( propertyId );
        if ( property == null )
        {
            return NotFound();
        }

        RoomType roomTypeEntity = createDto.ToRoomTypeFromCreate(propertyId);
        RoomType created = await _roomTypeRepository.Create( roomTypeEntity );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToRoomTypeDto() );
    }

    [HttpPut( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdateRoomTypeDto updateDto )
    {
        RoomType? existing = await _roomTypeRepository.GetById( id );
        if ( existing == null )
        {
            return NotFound();
        }

        RoomType roomTypeEntity = updateDto.ToRoomTypeFromUpdate( id, existing.PropertyId );
        RoomType updated = await _roomTypeRepository.Update( roomTypeEntity );

        return Ok( updated.ToRoomTypeDto() );
    }

    [HttpDelete( "roomtypes/{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id )
    {
        RoomType? roomType = await _roomTypeRepository.GetById( id );
        if ( roomType == null )
        {
            return NotFound();
        }

        await _roomTypeRepository.Delete( id );

        return NoContent();
    }
}
