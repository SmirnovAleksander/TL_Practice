using Api.Dto.RoomTypes;
using Infrastructure.Dto.RoomTypes;

namespace Api.Mappers.Service;

public static class RoomTypeServiceMappers
{
    public static CreateRoomTypeDto ToDto( this CreateRoomTypeRequest dto )
    {
        return new CreateRoomTypeDto
        {
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            Services = dto.Services,
            Amenities = dto.Amenities
        };
    }

    public static UpdateRoomTypeDto ToDto( this UpdateRoomTypeRequest dto, Guid id )
    {
        return new UpdateRoomTypeDto
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
