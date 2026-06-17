using Api.Dto.Properties;
using Api.Dto.RoomTypes;
using Api.Mappers.Entity;
using Api.Mappers.Service;
using Domain.Entities;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/properties" )]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IRoomTypeService _roomTypeService;

    public PropertiesController( IPropertyService propertyService, IRoomTypeService roomTypeService )
    {
        _propertyService = propertyService;
        _roomTypeService = roomTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken ct )
    {
        IReadOnlyList<Property> properties = await _propertyService.GetAllAsync( ct );
        List<PropertyResponse> propertyDtos = properties.Select( p => p.ToPropertyDto() ).ToList();

        return Ok( propertyDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id, CancellationToken ct )
    {
        Property property = await _propertyService.GetByIdAsync( id, ct );

        return Ok( property.ToPropertyDto() );
    }

    [HttpGet( "{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> GetRoomTypes( [FromRoute] Guid propertyId, CancellationToken ct )
    {
        IReadOnlyList<RoomType> roomTypes = await _roomTypeService.GetByPropertyAsync( propertyId, ct );
        List<RoomTypeResponse> roomTypeDtos = roomTypes.Select( r => r.ToRoomTypeDto() ).ToList();

        return Ok( roomTypeDtos );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreatePropertyRequest createDto, CancellationToken ct )
    {
        Property property = await _propertyService.CreateAsync( createDto.ToDto(), ct );

        return CreatedAtAction( nameof( GetById ), new { id = property.Id }, property.ToPropertyDto() );
    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdatePropertyRequest updateDto, CancellationToken ct )
    {
        Property updated = await _propertyService.UpdateAsync( updateDto.ToDto( id ), ct );

        return Ok( updated.ToPropertyDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id, CancellationToken ct )
    {
        await _propertyService.DeleteAsync( id, ct );

        return NoContent();
    }
}
