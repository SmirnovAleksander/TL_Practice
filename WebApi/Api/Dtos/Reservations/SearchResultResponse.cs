using Api.Dto.Property;
using Api.Dto.RoomType;
using Domain.Enums;

namespace Api.Dto.Reservation;

public class SearchResultResponse
{
    public required PropertyResponse Property { get; init; }
    public required RoomTypeResponse RoomType { get; init; }
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
}
