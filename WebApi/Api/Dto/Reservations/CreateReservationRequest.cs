using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Reservations;

public class CreateReservationRequest
{
    [Required( ErrorMessage = "PropertyId is required" )]
    public Guid PropertyId { get; init; }

    [Required( ErrorMessage = "RoomTypeId is required" )]
    public Guid RoomTypeId { get; init; }

    [Required( ErrorMessage = "ArrivalDate is required" )]
    public DateOnly ArrivalDate { get; init; }

    [Required( ErrorMessage = "DepartureDate is required" )]
    public DateOnly DepartureDate { get; init; }

    public TimeSpan? ArrivalTime { get; init; }
    public TimeSpan? DepartureTime { get; init; }

    [Required( ErrorMessage = "Guests count is required" )]
    [Range( 1, 100, ErrorMessage = "Guests must be between 1 and 100" )]
    public int Guests { get; init; }

    [Required( ErrorMessage = "GuestName is required" )]
    [MaxLength( 200, ErrorMessage = "GuestName can not be over 200 symbols" )]
    public string GuestName { get; init; } = string.Empty;

    [Phone( ErrorMessage = "Invalid phone number format" )]
    [MaxLength( 20, ErrorMessage = "Phone number can not be over 20 symbols" )]
    public string GuestPhoneNumber { get; init; } = string.Empty;
}
