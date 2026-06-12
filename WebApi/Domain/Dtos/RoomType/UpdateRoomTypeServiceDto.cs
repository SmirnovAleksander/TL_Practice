using Domain.Enums;

namespace Domain.Dtos.RoomType;

public class UpdateRoomTypeServiceDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal DailyPrice { get; init; }
    public Currency Currency { get; init; }
    public int MinPersonCount { get; init; }
    public int MaxPersonCount { get; init; }
    public List<string> Services { get; init; } = new List<string>();
    public List<string> Amenities { get; init; } = new List<string>();
}