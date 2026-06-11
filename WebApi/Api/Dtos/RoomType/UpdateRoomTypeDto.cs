using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Api.Dtos.RoomType;

public class UpdateRoomTypeDto
{
    [Required( ErrorMessage = "Name is required" )]
    [MaxLength( 100, ErrorMessage = "Name can not be over 100 symbols" )]
    public string Name { get; set; } = string.Empty;

    [Required( ErrorMessage = "DailyPrice is required" )]
    [Range( 0.01, double.MaxValue, ErrorMessage = "DailyPrice must be greater than 0" )]
    public decimal DailyPrice { get; set; }

    [Required( ErrorMessage = "Currency is required" )]
    [EnumDataType( typeof( Currency ), ErrorMessage = "Invalid currency value" )]
    public Currency Currency { get; set; }

    [Required( ErrorMessage = "MinPersonCount is required" )]
    [Range( 1, 100, ErrorMessage = "MinPersonCount must be between 1 and 100" )]
    public int MinPersonCount { get; set; }

    [Required( ErrorMessage = "MaxPersonCount is required" )]
    [Range( 1, 100, ErrorMessage = "MaxPersonCount must be between 1 and 100" )]
    public int MaxPersonCount { get; set; }

    public List<string> Services { get; set; } = new List<string>();
    public List<string> Amenities { get; set; } = new List<string>();
}
