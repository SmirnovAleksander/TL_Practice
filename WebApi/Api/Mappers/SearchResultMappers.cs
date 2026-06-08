using Api.Dtos.Reservation;
using Domain.Entities;

namespace Api.Mappers;

public static class SearchResultMappers
{
    public static SearchResultDto ToSearchResultDto(this RoomType roomType, Property property)
    {
        return new SearchResultDto
        {
            Property = property.ToPropertyDto(),
            RoomType = roomType.ToRoomTypeDto(),
            DailyPrice = roomType.DailyPrice,
            Currency = roomType.Currency
        };
    }
}
