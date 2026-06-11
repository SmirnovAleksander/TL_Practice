using Api.Dtos.Property;
using Api.Dtos.RoomType;
using Domain.Enums;

namespace Api.Dtos.Reservation;

public class SearchResultDto
{
    public PropertyDto Property { get; set; } = null!;
    public RoomTypeDto RoomType { get; set; } = null!;
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
}
