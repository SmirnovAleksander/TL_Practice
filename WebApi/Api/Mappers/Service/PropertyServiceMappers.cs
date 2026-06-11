using Api.Dtos.Property;
using Domain.Dtos.Property;

namespace Api.Mappers.Service;

public static class PropertyServiceMappers
{
    public static CreatePropertyServiceDto ToServiceDto( this CreatePropertyDto dto )
    {
        return new CreatePropertyServiceDto
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };
    }

    public static UpdatePropertyServiceDto ToServiceDto( this UpdatePropertyDto dto, Guid id )
    {
        return new UpdatePropertyServiceDto
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
