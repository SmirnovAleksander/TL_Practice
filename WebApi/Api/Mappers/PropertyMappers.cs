using Api.Dtos.Property;
using Domain.Entities;

namespace Api.Mappers;

public static class PropertyMappers
{
    public static PropertyDto ToPropertyDto(this Property property)
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

    public static Property ToPropertyFromCreate(this CreatePropertyDto dto)
    {
        return new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }

    public static Property ToPropertyFromUpdate(this UpdatePropertyDto dto)
    {
        return new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }
}
