using Api.Dto.Reservation;
using Infrastructure.Dto.Search;

namespace Api.Mappers.Entity;

public static class SearchResultMappers
{
    public static SearchResultResponse ToSearchResultDto( this SearchResultDto dto )
    {
        return new SearchResultResponse
        {
            Property = dto.Property.ToPropertyDto(),
            RoomType = dto.RoomType.ToRoomTypeDto(),
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency
        };
    }
}
