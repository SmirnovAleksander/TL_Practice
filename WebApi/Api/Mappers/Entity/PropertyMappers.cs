using Api.Dto.Properties;
using Domain.Entities;

namespace Api.Mappers.Entity;

public static class PropertyMappers
{
    public static PropertyResponse ToPropertyDto( this Property property )
    {
        return new PropertyResponse
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
