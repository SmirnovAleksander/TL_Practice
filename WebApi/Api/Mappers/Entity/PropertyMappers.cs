using Api.Dtos.Property;
using Domain.Entities;

namespace Api.Mappers.Entity;

public static class PropertyMappers
{
    public static PropertyDto ToPropertyDto( this Property property )
    {
        return new PropertyDto
        {
            Id = property.Id,
            Name = property.Name,
            Country = property.Country,
            City = property.City,
            Address = property.Address,
            Latitude = property.Latitude,
            Longitude = property.Longitude
        };
    }
}
