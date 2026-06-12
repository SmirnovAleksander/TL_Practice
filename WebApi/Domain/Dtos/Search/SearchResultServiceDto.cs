using Domain.Enums;

namespace Domain.Dtos.Search;

public class SearchResultServiceDto
{
    public required Entities.Property Property { get; init; }
    public required Entities.RoomType RoomType { get; init; }
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
}
