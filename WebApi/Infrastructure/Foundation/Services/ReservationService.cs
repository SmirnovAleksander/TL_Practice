using Infrastructure.Dto.Reservation;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository )
    {
        _reservationRepository = reservationRepository;
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
    }

    public async Task<IReadOnlyList<Reservation>> GetAllAsync( ReservationFilterDto filter, CancellationToken ct )
    {
        return await _reservationRepository.GetAllAsync(
            filter.PropertyId,
            filter.ArrivalDate,
            filter.DepartureDate,
            filter.GuestName,
            ct );
    }

    public async Task<Reservation> GetByIdAsync( Guid id, CancellationToken ct )
    {
        Reservation? reservation = await _reservationRepository.GetByIdAsync( id, ct );
        if ( reservation == null )
        {
            throw new NotFoundException( nameof( Reservation ), id );
        }

        return reservation;
    }

    public async Task<Reservation> CreateAsync( CreateReservationDto dto, CancellationToken ct )
    {
        RoomType roomType = await ValidateCreateAsync( dto, ct );

        int nights = dto.DepartureDate.DayNumber - dto.ArrivalDate.DayNumber;

        Reservation reservation = new Reservation
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            DepartureDate = dto.DepartureDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber,
            Total = roomType.DailyPrice * nights,
            Currency = roomType.Currency,
            IsCanceled = false
        };

        return await _reservationRepository.CreateAsync( reservation, ct );
    }

    public async Task CancelAsync( Guid id, CancellationToken ct )
    {
        Reservation? existing = await _reservationRepository.GetByIdAsync( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( nameof( Reservation ), id );
        }

        existing.IsCanceled = true;

        await _reservationRepository.UpdateAsync( existing, ct );
    }

    private async Task<RoomType> ValidateCreateAsync( CreateReservationDto dto, CancellationToken ct )
    {
        Property? property = await _propertyRepository.GetByIdAsync( dto.PropertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( nameof( Property ), dto.PropertyId );
        }

        RoomType? roomType = await _roomTypeRepository.GetByIdAsync( dto.RoomTypeId, ct );
        if ( roomType == null )
        {
            throw new NotFoundException( nameof( RoomType ), dto.RoomTypeId );
        }

        if ( dto.ArrivalDate >= dto.DepartureDate )
        {
            throw new ValidationException( "ArrivalDate can not be after DepartureDate" );
        }

        if ( dto.Guests < roomType.MinPersonCount || dto.Guests > roomType.MaxPersonCount )
        {
            throw new ValidationException( $"Guest count must be between {roomType.MinPersonCount} and {roomType.MaxPersonCount}" );
        }

        bool hasOverlap = await _reservationRepository.HasOverlapAsync(
            dto.RoomTypeId,
            dto.ArrivalDate,
            dto.DepartureDate,
            ct );

        if ( hasOverlap )
        {
            throw new ValidationException( "RoomType is not available for selected dates" );
        }

        return roomType;
    }
}
