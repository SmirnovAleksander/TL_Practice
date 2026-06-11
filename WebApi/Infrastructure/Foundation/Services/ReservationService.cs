using Domain.Dtos.Reservation;
using Domain.Dtos.Search;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

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

    public async Task<List<Reservation>> GetAllAsync( ReservationFilterServiceDto filter, CancellationToken ct = default )
    {
        return await _reservationRepository.GetAll(
            filter.PropertyId,
            filter.ArrivalDate,
            filter.DepartureDate,
            filter.GuestName,
            ct );
    }

    public async Task<Reservation> GetByIdAsync( Guid id, CancellationToken ct = default )
    {
        Reservation? reservation = await _reservationRepository.GetById( id, ct );
        if ( reservation == null )
        {
            throw new NotFoundException( "Reservation", id );
        }

        return reservation;
    }

    public async Task<Reservation> CreateAsync( CreateReservationServiceDto dto, CancellationToken ct = default )
    {
        Property? property = await _propertyRepository.GetById( dto.PropertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( "Property", dto.PropertyId );
        }

        RoomType? roomType = await _roomTypeRepository.GetById( dto.RoomTypeId, ct );
        if ( roomType == null )
        {
            throw new NotFoundException( "RoomType", dto.RoomTypeId );
        }

        if ( dto.ArrivalDate >= dto.DepartureDate )
        {
            throw new ValidationDomainException( "ArrivalDate can not be after DepartureDate" );
        }

        if ( dto.Guests < roomType.MinPersonCount || dto.Guests > roomType.MaxPersonCount )
        {
            throw new ValidationDomainException( $"Guest count must be between {roomType.MinPersonCount} and {roomType.MaxPersonCount}" );
        }

        bool hasOverlap = await _reservationRepository.HasOverlap(
            dto.RoomTypeId,
            dto.ArrivalDate,
            dto.DepartureDate,
            ct );

        if ( hasOverlap )
        {
            throw new ValidationDomainException( "RoomType is not available for selected dates" );
        }

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

        return await _reservationRepository.Create( reservation, ct );
    }

    public async Task CancelAsync( Guid id, CancellationToken ct = default )
    {
        Reservation? existing = await _reservationRepository.GetById( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( "Reservation", id );
        }

        await _reservationRepository.Cancel( id, ct );
    }

    public async Task<List<SearchResultServiceDto>> SearchAsync( SearchFilterServiceDto filter, CancellationToken ct = default )
    {
        List<Property> allProperties = await _propertyRepository.GetAll( ct );

        if ( !string.IsNullOrWhiteSpace( filter.City ) )
        {
            allProperties = allProperties
                .Where( p => p.City.Contains( filter.City, StringComparison.OrdinalIgnoreCase ) )
                .ToList();
        }

        List<SearchResultServiceDto> results = new List<SearchResultServiceDto>();

        foreach ( Property property in allProperties )
        {
            List<RoomType> roomTypes = await _roomTypeRepository.GetByProperty( property.Id, ct );

            if ( filter.Guests.HasValue )
            {
                roomTypes = roomTypes
                    .Where( r => r.MinPersonCount <= filter.Guests && r.MaxPersonCount >= filter.Guests )
                    .ToList();
            }

            if ( filter.MaxPrice.HasValue )
            {
                roomTypes = roomTypes
                    .Where( r => r.DailyPrice <= filter.MaxPrice )
                    .ToList();
            }

            foreach ( RoomType roomType in roomTypes )
            {
                if ( filter.ArrivalDate.HasValue && filter.DepartureDate.HasValue )
                {
                    bool hasOverlap = await _reservationRepository.HasOverlap(
                        roomType.Id,
                        filter.ArrivalDate.Value,
                        filter.DepartureDate.Value,
                        ct );

                    if ( hasOverlap )
                    {
                        continue;
                    }
                }

                results.Add( new SearchResultServiceDto
                {
                    Property = property,
                    RoomType = roomType,
                    DailyPrice = roomType.DailyPrice,
                    Currency = roomType.Currency
                } );
            }
        }

        return results;
    }
}
