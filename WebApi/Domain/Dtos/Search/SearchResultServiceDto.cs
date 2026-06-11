using Domain.Enums;

namespace Domain.Dtos.Search;

public class SearchResultServiceDto
{
    public Entities.Property Property { get; set; } = null!;
    public Entities.RoomType RoomType { get; set; } = null!;
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
}
