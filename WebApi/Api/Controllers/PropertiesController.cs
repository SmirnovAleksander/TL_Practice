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
    public IActionResult GetAll()
    {
        List<Property> properties = _propertyRepository.GetAll();
        List<PropertyDto> propertyDtos = properties.Select( p => p.ToPropertyDto() ).ToList();

        return Ok( propertyDtos );
    }

    [HttpGet( "{id}" )]
    public IActionResult GetById( [FromRoute] Guid id )
    {
        Property? property = _propertyRepository.GetById( id );
        if ( property == null )
        {
            return NotFound();
        }

        return Ok( property.ToPropertyDto() );
    }

    [HttpPost]
    public IActionResult Create( [FromBody] CreatePropertyDto createDto )
    {
        Property property = _propertyRepository.Create( createDto.ToPropertyFromCreate() );

        return CreatedAtAction( nameof( GetById ), new { id = property.Id }, property.ToPropertyDto() );
    }

    [HttpPut( "{id}" )]
    public IActionResult Update( [FromRoute] Guid id, [FromBody] UpdatePropertyDto updateDto )
    {
        Property? existing = _propertyRepository.GetById( id );
        if ( existing == null )
        {
            return NotFound();
        }

        Property updatedEntity = updateDto.ToPropertyFromUpdate();
        updatedEntity.Id = existing.Id;

        Property updated = _propertyRepository.Update( updatedEntity );

        return Ok( updated.ToPropertyDto() );
    }

    [HttpDelete( "{id}" )]
    public IActionResult Delete( [FromRoute] Guid id )
    {
        Property? property = _propertyRepository.GetById( id );
        if ( property == null )
        {
            return NotFound();
        }

        _propertyRepository.Delete( id );

        return NoContent();
    }
}
