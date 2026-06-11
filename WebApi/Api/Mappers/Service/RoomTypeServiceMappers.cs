using Api.Dtos.RoomType;
using Domain.Dtos.RoomType;

namespace Api.Mappers.Service;

public static class RoomTypeServiceMappers
{
    public static CreateRoomTypeServiceDto ToServiceDto( this CreateRoomTypeDto dto, Guid propertyId )
    {
        return new CreateRoomTypeServiceDto
        {
            PropertyId = propertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            Services = dto.Services,
            Amenities = dto.Amenities
        };
    }

    public static UpdateRoomTypeServiceDto ToServiceDto( this UpdateRoomTypeDto dto, Guid id )
    {
        return new UpdateRoomTypeServiceDto
        {
            Id = id,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            Services = dto.Services,
            Amenities = dto.Amenities
        };
    }
}
