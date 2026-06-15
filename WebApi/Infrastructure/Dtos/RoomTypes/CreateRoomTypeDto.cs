using Domain.Enums;

namespace Infrastructure.Dto.RoomType;

public class CreateRoomTypeDto
{
    public Guid PropertyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
    public int MinPersonCount { get; init; }
    public int MaxPersonCount { get; init; }
    public List<string> Services { get; init; } = [];
    public List<string> Amenities { get; init; } = [];
}