using Api.Dtos.Property;
using Api.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/properties" )]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    public PropertiesController( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Property> properties = await _propertyRepository.GetAll();
        List<PropertyDto> propertyDtos = properties.Select( p => p.ToPropertyDto() ).ToList();

        return Ok( propertyDtos );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid id )
    {
        Property? property = await _propertyRepository.GetById( id );
        if ( property == null )
        {
            return NotFound();
        }

        return Ok( property.ToPropertyDto() );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreatePropertyDto createDto )
    {
        Property property = await _propertyRepository.Create( createDto.ToPropertyFromCreate() );

        return CreatedAtAction( nameof( GetById ), new { id = property.Id }, property.ToPropertyDto() );
    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> Update( [FromRoute] Guid id, [FromBody] UpdatePropertyDto updateDto )
    {
        Property? existing = await _propertyRepository.GetById( id );
        if ( existing == null )
        {
            return NotFound();
        }

        Property propertyEntity = updateDto.ToPropertyFromUpdate( id );
        Property updated = await _propertyRepository.Update( propertyEntity );

        return Ok( updated.ToPropertyDto() );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id )
    {
        Property? property = await _propertyRepository.GetById( id );
        if ( property == null )
        {
            return NotFound();
        }

        await _propertyRepository.Delete( id );

        return NoContent();
    }
}
