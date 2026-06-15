namespace Infrastructure.Dto.Reservation;

public class ReservationFilterDto
{
    public Guid? PropertyId { get; init; }
    public DateOnly? ArrivalDate { get; init; }
    public DateOnly? DepartureDate { get; init; }
    public string? GuestName { get; init; }
}