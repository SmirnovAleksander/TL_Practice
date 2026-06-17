using Api.Dto.Properties;
using Infrastructure.Dto.Properties;

namespace Api.Mappers.Service;

public static class PropertyServiceMappers
{
    public static CreatePropertyDto ToDto( this CreatePropertyRequest dto )
    {
        return new CreatePropertyDto
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }

    public static UpdatePropertyDto ToDto( this UpdatePropertyRequest dto, Guid id )
    {
        return new UpdatePropertyDto
        {
            Id = id,
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }
}
