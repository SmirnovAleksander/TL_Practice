using Domain.Enums;

namespace Domain.Dtos.Search;

public class SearchResultServiceDto
{
    public required Entities.Property Property { get; set; }
    public required Entities.RoomType RoomType { get; set; }
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
}
