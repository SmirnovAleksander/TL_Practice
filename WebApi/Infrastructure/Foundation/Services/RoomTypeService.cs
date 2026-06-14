using Domain.Dtos.RoomType;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

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

    public async Task<IReadOnlyList<RoomType>> GetByPropertyAsync( Guid propertyId, CancellationToken ct = default )
    {
        Property? property = await _propertyRepository.GetByIdAsync( propertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( "Property", propertyId );
        }

        return await _roomTypeRepository.GetByPropertyAsync( propertyId, null, null, ct );
    }

    public async Task<RoomType> GetByIdAsync( Guid id, CancellationToken ct = default )
    {
        RoomType? roomType = await _roomTypeRepository.GetByIdAsync( id, ct );
        if ( roomType == null )
        {
            throw new NotFoundException( "RoomType", id );
        }

        return roomType;
    }

    public async Task<RoomType> CreateAsync( CreateRoomTypeServiceDto dto, CancellationToken ct = default )
    {
        Property? property = await _propertyRepository.GetByIdAsync( dto.PropertyId, ct );
        if ( property == null )
        {
            throw new NotFoundException( "Property", dto.PropertyId );
        }

        if ( string.IsNullOrWhiteSpace( dto.Name ) )
        {
            throw new ValidationDomainException( "RoomType name is required" );
        }

        if ( dto.DailyPrice <= 0 )
        {
            throw new ValidationDomainException( "DailyPrice must be greater than 0" );
        }

        if ( dto.MinPersonCount <= 0 )
        {
            throw new ValidationDomainException( "MinPersonCount must be greater than 0" );
        }

        if ( dto.MaxPersonCount < dto.MinPersonCount )
        {
            throw new ValidationDomainException( "MaxPersonCount must be >= MinPersonCount" );
        }

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

    public async Task<RoomType> UpdateAsync( UpdateRoomTypeServiceDto dto, CancellationToken ct = default )
    {
        RoomType? existing = await _roomTypeRepository.GetByIdAsync( dto.Id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( "RoomType", dto.Id );
        }

        if ( string.IsNullOrWhiteSpace( dto.Name ) )
        {
            throw new ValidationDomainException( "RoomType name is required" );
        }

        if ( dto.DailyPrice <= 0 )
        {
            throw new ValidationDomainException( "DailyPrice must be great than 0" );
        }

        existing.Name = dto.Name;
        existing.DailyPrice = dto.DailyPrice;
        existing.Currency = dto.Currency;
        existing.MinPersonCount = dto.MinPersonCount;
        existing.MaxPersonCount = dto.MaxPersonCount;
        existing.Services = dto.Services;
        existing.Amenities = dto.Amenities;

        return await _roomTypeRepository.UpdateAsync( existing, ct );
    }

    public async Task DeleteAsync( Guid id, CancellationToken ct = default )
    {
        RoomType? existing = await _roomTypeRepository.GetByIdAsync( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( "RoomType", id );
        }

        await _roomTypeRepository.DeleteAsync( existing, ct );
    }
}
