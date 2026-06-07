using Domain.Entities;
using Infrastructure.Foundation.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route( "api/properties" )]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly HotelManagementDbContext _context;
    public PropertiesController( HotelManagementDbContext context )
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Property> properties = _context.Properties.ToList();

        return Ok( properties );
    }

    [HttpGet( "{id}" )]
    public IActionResult GetById( [FromRoute] Guid id )
    {
        Property? property = _context.Properties.Find( id );
        if ( property == null )
        {
            return NotFound();
        }

        return Ok( property );
    }

    [HttpPost]
    public IActionResult Create( [FromBody] Property property )
    {
        property.Id = Guid.NewGuid();
        _context.Properties.Add( property );
        _context.SaveChanges();

        return CreatedAtAction( nameof( GetById ), new { id = property.Id }, property );
    }

    [HttpPut( "{id}" )]
    public IActionResult Update( [FromRoute] Guid id, [FromBody] Property updated )
    {
        Property? property = _context.Properties.Find( id );
        if ( property == null )
        {
            return NotFound();
        }

        property.Name = updated.Name;
        property.Country = updated.Country;
        property.City = updated.City;
        property.Address = updated.Address;
        property.Latitude = updated.Latitude;
        property.Longitude = updated.Longitude;

        _context.SaveChanges();

        return Ok( property );
    }

    [HttpDelete( "{id}" )]
    public IActionResult Delete( [FromRoute] Guid id )
    {
        Property? property = _context.Properties.Find( id );
        if ( property == null )
        {
            return NotFound();
        }

        _context.Properties.Remove( property );
        _context.SaveChanges();

        return NoContent();
    }
}
