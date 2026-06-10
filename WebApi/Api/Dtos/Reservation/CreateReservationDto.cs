using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Reservation;

public class CreateReservationDto
{
    [Required( ErrorMessage = "PropertyId is required" )]
    public Guid PropertyId { get; set; }

    [Required( ErrorMessage = "RoomTypeId is required" )]
    public Guid RoomTypeId { get; set; }

    [Required( ErrorMessage = "ArrivalDate is required" )]
    public DateOnly ArrivalDate { get; set; }

    [Required( ErrorMessage = "DepartureDate is required" )]
    public DateOnly DepartureDate { get; set; }

    public TimeSpan? ArrivalTime { get; set; }
    public TimeSpan? DepartureTime { get; set; }

    [Required( ErrorMessage = "Guests count is required" )]
    [Range( 1, 100, ErrorMessage = "Guests must be between 1 and 100" )]
    public int Guests { get; set; }

    [Required( ErrorMessage = "GuestName is required" )]
    [MaxLength( 200, ErrorMessage = "GuestName can not be over 200 symbols" )]
    public string GuestName { get; set; } = string.Empty;

    [Phone( ErrorMessage = "Invalid phone number format" )]
    [MaxLength( 20, ErrorMessage = "Phone number can not be over 20 symbols" )]
    public string GuestPhoneNumber { get; set; } = string.Empty;
}
