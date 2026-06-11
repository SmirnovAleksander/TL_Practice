using Api.Dtos.Property;
using Api.Dtos.RoomType;
using Domain.Enums;

namespace Api.Dtos.Reservation;

public class SearchResultDto
{
    public required PropertyDto Property { get; set; }
    public required RoomTypeDto RoomType { get; set; }
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
}
