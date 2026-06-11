using Domain.Enums;

namespace Api.Dtos.RoomType;

public class RoomTypeDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public List<string> Services { get; set; } = new List<string>();
    public List<string> Amenities { get; set; } = new List<string>();
}
