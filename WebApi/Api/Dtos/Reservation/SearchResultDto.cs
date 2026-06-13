using Api.Dtos.Property;
using Api.Dtos.RoomType;
using Domain.Enums;

namespace Api.Dtos.Reservation;

public class SearchResultDto
{
    public required PropertyDto Property { get; init; }
    public required RoomTypeDto RoomType { get; init; }
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
}
