namespace Domain.Dtos.Reservation;

public class ReservationFilterServiceDto
{
    public Guid? PropertyId { get; init; }
    public DateOnly? ArrivalDate { get; init; }
    public DateOnly? DepartureDate { get; init; }
    public string? GuestName { get; init; }
}