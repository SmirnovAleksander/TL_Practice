using Api.Dtos.Property;
using Api.Mappers.Entity;
using Api.Mappers.Service;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/properties" )]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    public PropertiesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( CancellationToken ct )
    {
        List<Property> properties = await _propertyService.GetAllAsync( ct );
        List<PropertyDto> propertyDtos = properties.Select( p => p.ToPropertyDto() ).ToList();

        return Ok( propertyDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id, CancellationToken ct )
    {
        Property property = await _propertyService.GetByIdAsync( id, ct );

        return Ok( property.ToPropertyDto() );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreatePropertyDto createDto, CancellationToken ct )
    {
        Property property = await _propertyService.CreateAsync( createDto.ToServiceDto(), ct );

        return CreatedAtAction( nameof( GetById ), new { id = property.Id }, property.ToPropertyDto() );
    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdatePropertyDto updateDto, CancellationToken ct )
    {
        Property updated = await _propertyService.UpdateAsync( updateDto.ToServiceDto( id ), ct );

        return Ok( updated.ToPropertyDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id, CancellationToken ct )
    {
        await _propertyService.DeleteAsync( id, ct );

        return NoContent();
    }
}
