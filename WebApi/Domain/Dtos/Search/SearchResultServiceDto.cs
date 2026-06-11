namespace Domain.Dtos.Search;

public class SearchResultServiceDto
{
    public Entities.Property Property { get; set; } = null!;
    public Entities.RoomType RoomType { get; set; } = null!;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
}
