using Api.Dto.Reservations;
using Infrastructure.Dto.Reservations;

namespace Api.Mappers.Service;

public static class ReservationServiceMappers
{
    public static CreateReservationDto ToDto( this CreateReservationRequest dto )
    {
        return new CreateReservationDto
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
