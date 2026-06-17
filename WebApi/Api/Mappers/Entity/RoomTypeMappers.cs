using Api.Dto.RoomTypes;
using Domain.Entities;

namespace Api.Mappers.Entity;

public static class RoomTypeMappers
{
    public static RoomTypeResponse ToRoomTypeDto( this RoomType roomType )
    {
        return new RoomTypeResponse
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
}
