using Api.Dtos.Reservation;
using Domain.Entities;

namespace Api.Mappers;

public static class ReservationMappers
{
    public static ReservationDto ToReservationDto( this Reservation reservation )
    {
        return new ReservationDto
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

    public static Reservation ToReservationFromCreate( this CreateReservationDto dto )
    {
        return new Reservation
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            DepartureDate = dto.DepartureDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber
        };
    }
}
