using Api.Dtos.Reservation;
using Domain.Dtos.Search;

namespace Api.Mappers.Entity;

public static class SearchResultMappers
{
    public static SearchResultDto ToSearchResultDto( this SearchResultServiceDto dto )
    {
        return new SearchResultDto
        {
            Property = dto.Property.ToPropertyDto(),
            RoomType = dto.RoomType.ToRoomTypeDto(),
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency
        };
    }
}
