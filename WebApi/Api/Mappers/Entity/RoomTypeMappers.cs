using Api.Dtos.RoomType;
using Domain.Entities;

namespace Api.Mappers.Entity;

public static class RoomTypeMappers
{
    public static RoomTypeDto ToRoomTypeDto( this RoomType roomType )
    {
        return new RoomTypeDto
        {
            Id = roomType.Id,
            PropertyId = roomType.PropertyId,
            Name = roomType.Name,
            DailyPrice = roomType.DailyPrice,
            Currency = roomType.Currency,
            MinPersonCount = roomType.MinPersonCount,
            MaxPersonCount = roomType.MaxPersonCount,
            Services = roomType.Services,
            Amenities = roomType.Amenities
        };
    }

    public static RoomType ToRoomTypeFromCreate( this CreateRoomTypeDto dto, Guid propertyId )
    {
        return new RoomType
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

    public static RoomType ToRoomTypeFromUpdate( this UpdateRoomTypeDto dto, Guid id, Guid propertyId )
    {
        return new RoomType
        {
            Id = id,
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
}
