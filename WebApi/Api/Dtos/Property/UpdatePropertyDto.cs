using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Property;

public class UpdatePropertyDto
{
    [Required( ErrorMessage = "Name is required" )]
    [MaxLength( 200, ErrorMessage = "Name can not be over 200 symbols" )]
    public string Name { get; set; } = string.Empty;

    [MaxLength( 100, ErrorMessage = "Country can not be over 100 symbols" )]
    public string Country { get; set; } = string.Empty;

    [Required( ErrorMessage = "City is required" )]
    [MaxLength( 100, ErrorMessage = "City can not be over 100 symbols" )]
    public string City { get; set; } = string.Empty;

    [MaxLength( 200, ErrorMessage = "Address can not be over 200 symbols" )]
    public string Address { get; set; } = string.Empty;

    [Range( -90, 90, ErrorMessage = "Latitude must be between -90 and 90" )]
    public decimal Latitude { get; set; }

    [Range( -180, 180, ErrorMessage = "Longitude must be between -180 and 180" )]
    public decimal Longitude { get; set; }
}
