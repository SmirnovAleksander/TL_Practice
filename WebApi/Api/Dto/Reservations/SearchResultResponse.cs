using Api.Dto.Properties;
using Api.Dto.RoomTypes;
using Domain.Enums;

namespace Api.Dto.Reservations;

public class SearchResultResponse
{
    public required PropertyResponse Property { get; init; }
    public required RoomTypeResponse RoomType { get; init; }
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
}
