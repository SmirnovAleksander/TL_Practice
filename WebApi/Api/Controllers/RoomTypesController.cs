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
    public IActionResult GetByProperty( [FromRoute] Guid propertyId )
    {
        Property? property = _propertyRepository.GetById( propertyId );
        if ( property == null )
        {
            return NotFound();
        }

        List<RoomType> roomTypes = _roomTypeRepository.GetByProperty( propertyId );
        List<RoomTypeDto> roomTypeDtos = roomTypes.Select( rt => rt.ToRoomTypeDto() ).ToList();

        return Ok( roomTypeDtos );
    }

    [HttpGet( "roomtypes/{id:guid}" )]
    public IActionResult GetById( [FromRoute] Guid id )
    {
        RoomType? roomType = _roomTypeRepository.GetById( id );
        if ( roomType == null )
        {
            return NotFound();
        }

        return Ok( roomType.ToRoomTypeDto() );
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public IActionResult Create( [FromRoute] Guid propertyId, [FromBody] CreateRoomTypeDto createDto )
    {
        Property? property = _propertyRepository.GetById( propertyId );
        if ( property == null )
        {
            return NotFound();
        }

        RoomType roomTypeEntity = createDto.ToRoomTypeFromCreate(propertyId);
        RoomType created = _roomTypeRepository.Create( roomTypeEntity );

        return CreatedAtAction( nameof( GetById ), new { id = created.Id }, created.ToRoomTypeDto() );
    }

    [HttpPut( "roomtypes/{id:guid}" )]
    public IActionResult Update( [FromRoute] Guid id, [FromBody] UpdateRoomTypeDto updateDto )
    {
        RoomType? existing = _roomTypeRepository.GetById( id );
        if ( existing == null )
        {
            return NotFound();
        }

        RoomType roomTypeEntity = updateDto.ToRoomTypeFromUpdate( id, existing.PropertyId );
        RoomType updated = _roomTypeRepository.Update( roomTypeEntity );

        return Ok( updated.ToRoomTypeDto() );
    }

    [HttpDelete( "roomtypes/{id:guid}" )]
    public IActionResult Delete( [FromRoute] Guid id )
    {
        RoomType? roomType = _roomTypeRepository.GetById( id );
        if ( roomType == null )
        {
            return NotFound();
        }

        _roomTypeRepository.Delete( id );

        return NoContent();
    }
}
