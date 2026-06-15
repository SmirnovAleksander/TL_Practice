using Infrastructure.Dto.Property;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct )
    {
        return await _propertyRepository.GetAllAsync( ct: ct );
    }

    public async Task<Property> GetByIdAsync( Guid id, CancellationToken ct )
    {
        return await GetExistingPropertyAsync( id, ct );
    }

    public async Task<Property> CreateAsync( CreatePropertyDto dto, CancellationToken ct )
    {
        ValidatePropertyData( dto.Name, dto.City );

        Property property = new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };

        return await _propertyRepository.CreateAsync( property, ct );
    }

    public async Task<Property> UpdateAsync( UpdatePropertyDto dto, CancellationToken ct )
    {
        Property existing = await GetExistingPropertyAsync( dto.Id, ct );
        ValidatePropertyData( dto.Name, dto.City );

        existing.Name = dto.Name;
        existing.Country = dto.Country;
        existing.City = dto.City;
        existing.Address = dto.Address;
        existing.Latitude = dto.Latitude;
        existing.Longitude = dto.Longitude;

        return await _propertyRepository.UpdateAsync( existing, ct );
    }

    public async Task DeleteAsync( Guid id, CancellationToken ct )
    {
        Property existing = await GetExistingPropertyAsync( id, ct );
        await _propertyRepository.DeleteAsync( existing, ct );
    }

    private async Task<Property> GetExistingPropertyAsync( Guid id, CancellationToken ct )
    {
        Property? existing = await _propertyRepository.GetByIdAsync( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( nameof( Property ), id );
        }

        return existing;
    }

    private static void ValidatePropertyData( string name, string city )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ValidationException( "Property name is required" );
        }

        if ( string.IsNullOrWhiteSpace( city ) )
        {
            throw new ValidationException( "City is required" );
        }
    }
}
