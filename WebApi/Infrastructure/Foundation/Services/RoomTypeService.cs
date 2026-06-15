using Infrastructure.Dto.RoomType;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;

    public RoomTypeService( IRoomTypeRepository roomTypeRepository, IPropertyRepository propertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IReadOnlyList<RoomType>> GetByPropertyAsync( Guid propertyId, CancellationToken ct )
    {
        Property? property = await _propertyRepository.GetByIdAsync( propertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( nameof( Property ), propertyId );
        }

        return await _roomTypeRepository.GetByPropertyAsync( propertyId, guests: null, maxPrice: null, ct: ct );
    }

    public async Task<RoomType> GetByIdAsync( Guid id, CancellationToken ct )
    {
        RoomType? roomType = await _roomTypeRepository.GetByIdAsync( id, ct );
        if ( roomType == null )
        {
            throw new NotFoundException( nameof( RoomType ), id );
        }

        return roomType;
    }

    public async Task<RoomType> CreateAsync( CreateRoomTypeDto dto, CancellationToken ct )
    {
        Property? property = await _propertyRepository.GetByIdAsync( dto.PropertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( nameof( Property ), dto.PropertyId );
        }

        ValidateRoomTypeData(
            dto.Name,
            dto.DailyPrice,
            dto.MinPersonCount,
            dto.MaxPersonCount );

        RoomType roomType = new RoomType
        {
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            Services = dto.Services,
            Amenities = dto.Amenities
        };

        return await _roomTypeRepository.CreateAsync( roomType, ct );
    }

    public async Task<RoomType> UpdateAsync( UpdateRoomTypeDto dto, CancellationToken ct )
    {
        RoomType? existing = await _roomTypeRepository.GetByIdAsync( dto.Id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( nameof( RoomType ), dto.Id );
        }

        ValidateRoomTypeData(
            dto.Name,
            dto.DailyPrice,
            dto.MinPersonCount,
            dto.MaxPersonCount );

        existing.Name = dto.Name;
        existing.DailyPrice = dto.DailyPrice;
        existing.Currency = dto.Currency;
        existing.MinPersonCount = dto.MinPersonCount;
        existing.MaxPersonCount = dto.MaxPersonCount;
        existing.Services = dto.Services;
        existing.Amenities = dto.Amenities;

        return await _roomTypeRepository.UpdateAsync( existing, ct );
    }

    public async Task DeleteAsync( Guid id, CancellationToken ct )
    {
        RoomType? existing = await _roomTypeRepository.GetByIdAsync( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( nameof( RoomType ), id );
        }

        await _roomTypeRepository.DeleteAsync( existing, ct );
    }

    private static void ValidateRoomTypeData(
        string name,
        decimal dailyPrice,
        int minPersonCount,
        int maxPersonCount )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ValidationException( "RoomType name is required" );
        }

        if ( dailyPrice <= 0 )
        {
            throw new ValidationException( "DailyPrice must be greater than 0" );
        }

        if ( minPersonCount <= 0 )
        {
            throw new ValidationException( "MinPersonCount must be greater than 0" );
        }

        if ( maxPersonCount < minPersonCount )
        {
            throw new ValidationException( "MaxPersonCount must be >= MinPersonCount" );
        }
    }
}
