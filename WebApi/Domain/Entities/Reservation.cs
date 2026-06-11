using Domain.Enums;

namespace Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeSpan? ArrivalTime { get; set; }
    public TimeSpan? DepartureTime { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestPhoneNumber { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public Currency Currency { get; set; }
    public bool IsCanceled { get; set; }
}
