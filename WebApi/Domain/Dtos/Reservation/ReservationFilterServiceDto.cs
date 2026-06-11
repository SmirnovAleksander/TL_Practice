namespace Domain.Dtos.Reservation;

public class ReservationFilterServiceDto
{
    public Guid? PropertyId { get; set; }
    public DateOnly? ArrivalDate { get; set; }
    public DateOnly? DepartureDate { get; set; }
    public string? GuestName { get; set; }
}