using Api.Dtos.Reservation;
using Domain.Dtos.Reservation;

namespace Api.Mappers.Service;

public static class ReservationServiceMappers
{
    public static CreateReservationServiceDto ToServiceDto( this CreateReservationDto dto )
    {
        return new CreateReservationServiceDto
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            DepartureDate = dto.DepartureDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureTime = dto.DepartureTime,
            Guests = dto.Guests,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber
        };
    }
}
