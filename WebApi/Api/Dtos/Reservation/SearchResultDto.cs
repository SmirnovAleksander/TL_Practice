using Api.Dtos.Property;
using Api.Dtos.RoomType;

namespace Api.Dtos.Reservation;

public class SearchResultDto
{
    public PropertyDto Property { get; set; } = null!;
    public RoomTypeDto RoomType { get; set; } = null!;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
}
