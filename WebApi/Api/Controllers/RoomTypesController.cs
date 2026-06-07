using Domain.Entities;
using Infrastructure.Foundation.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api")]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly HotelManagementDbContext _context;
    public RoomTypesController( HotelManagementDbContext context )
    {
        _context = context;
    }

    [HttpGet("properties/{propertyId}/roomtypes")]
    public IActionResult GetByProperty([FromRoute] Guid propertyId)
    {
        Property? property = _context.Properties.Find(propertyId);
        if ( property == null )
        {
            return NotFound();
        }

        List<RoomType> roomTypes = _context.RoomTypes.Where(r => r.PropertyId == propertyId).ToList();

        return Ok(roomTypes);
    }

    [HttpGet("roomtypes/{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        RoomType? roomType = _context.RoomTypes.Find(id);
        if ( roomType == null )
        {
            return NotFound();
        }

        return Ok(roomType);
    }

    [HttpPost("properties/{propertyId}/roomtypes")]
    public IActionResult Create([FromRoute] Guid propertyId, [FromBody] RoomType roomType)
    {
        Property? property = _context.Properties.Find(propertyId);
        if ( property == null )
        {
            return NotFound();
        }

        roomType.Id = Guid.NewGuid();
        roomType.PropertyId = propertyId;

        _context.RoomTypes.Add(roomType);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = roomType.Id }, roomType);
    }

    [HttpPut("roomtypes/{id}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RoomType updated)
    {
        RoomType? roomType = _context.RoomTypes.Find(id);
        if ( roomType == null )
        {
            return NotFound();
        }

        roomType.Name = updated.Name;
        roomType.DailyPrice = updated.DailyPrice;
        roomType.Currency = updated.Currency;
        roomType.MinPersonCount = updated.MinPersonCount;
        roomType.MaxPersonCount = updated.MaxPersonCount;
        roomType.Services = updated.Services;
        roomType.Amenities = updated.Amenities;

        _context.SaveChanges();

        return Ok(roomType);
    }

    [HttpDelete("roomtypes/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        RoomType? roomType = _context.RoomTypes.Find(id);
        if ( roomType == null )
        {
            return NotFound();
        }

        _context.RoomTypes.Remove(roomType);
        _context.SaveChanges();
        
        return NoContent();
    }
}
