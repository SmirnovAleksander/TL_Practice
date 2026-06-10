using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.RoomType;

public class UpdateRoomTypeDto
{
    [Required( ErrorMessage = "Name is required" )]
    [MaxLength( 100, ErrorMessage = "Name can not be over 100 symbols" )]
    public string Name { get; set; } = string.Empty;

    [Required( ErrorMessage = "DailyPrice is required" )]
    [Range( 0.01, double.MaxValue, ErrorMessage = "DailyPrice must be greater than 0" )]
    public decimal DailyPrice { get; set; }

    [MaxLength( 10, ErrorMessage = "Currency can not be over 10 symbols" )]
    public string Currency { get; set; } = string.Empty;

    [Required( ErrorMessage = "MinPersonCount is required" )]
    [Range( 1, 100, ErrorMessage = "MinPersonCount must be between 1 and 100" )]
    public int MinPersonCount { get; set; }

    [Required( ErrorMessage = "MaxPersonCount is required" )]
    [Range( 1, 100, ErrorMessage = "MaxPersonCount must be between 1 and 100" )]
    public int MaxPersonCount { get; set; }

    public List<string> Services { get; set; } = new List<string>();
    public List<string> Amenities { get; set; } = new List<string>();
}
