using Domain.Enums;

namespace Infrastructure.Dto.Search;

public class SearchResultDto
{
    public required Domain.Entities.Property Property { get; init; }
    public required Domain.Entities.RoomType RoomType { get; init; }
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
}
