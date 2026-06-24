using Api.Dto.Reservations;
using Domain.Entities;

namespace Api.Mappers.Entity;

public static class ReservationMappers
{
    public static ReservationResponse ToReservationDto( this Reservation reservation )
    {
        return new ReservationResponse
        {
            Id = reservation.Id,
            PropertyId = reservation.PropertyId,
            RoomTypeId = reservation.RoomTypeId,
            ArrivalDate = reservation.ArrivalDate,
            DepartureDate = reservation.DepartureDate,
            ArrivalTime = reservation.ArrivalTime,
            DepartureTime = reservation.DepartureTime,
            GuestName = reservation.GuestName,
            GuestPhoneNumber = reservation.GuestPhoneNumber,
            Total = reservation.Total,
            Currency = reservation.Currency,
            IsCanceled = reservation.IsCanceled
        };
    }
}
