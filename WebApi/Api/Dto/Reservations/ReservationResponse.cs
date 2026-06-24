using Domain.Enums;

namespace Api.Dto.Reservations;

public class ReservationResponse
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public Guid RoomTypeId { get; init; }
    public DateOnly ArrivalDate { get; init; }
    public DateOnly DepartureDate { get; init; }
    public TimeSpan? ArrivalTime { get; init; }
    public TimeSpan? DepartureTime { get; init; }
    public string GuestName { get; init; } = string.Empty;
    public string GuestPhoneNumber { get; init; } = string.Empty;
    public decimal Total { get; init; }
    public Currency Currency { get; init; }
    public bool IsCanceled { get; init; }
}
