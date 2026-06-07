namespace Api.Dtos.RoomType;

public class UpdateRoomTypeDto
{
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public List<string> Services { get; set; } = new List<string>();
    public List<string> Amenities { get; set; } = new List<string>();
}
