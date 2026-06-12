
using Domain.Dtos.Property;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService( IPropertyRepository propertyRepository )
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct = default )
    {
        return await _propertyRepository.GetAll( ct );
    }

    public async Task<Property> GetByIdAsync( Guid id, CancellationToken ct = default )
    {
        Property? property = await _propertyRepository.GetById( id, ct );
        if ( property == null )
        {
            throw new NotFoundException( "Property", id );
        }

        return property;
    }

    public async Task<Property> CreateAsync( CreatePropertyServiceDto dto, CancellationToken ct = default )
    {
        if ( string.IsNullOrWhiteSpace( dto.Name ) )
        {
            throw new ValidationDomainException( "Property name is required" );
        }

        if ( string.IsNullOrWhiteSpace( dto.City ) )
        {
            throw new ValidationDomainException( "City is required" );
        }

        Property property = new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
        };

        return await _propertyRepository.Create( property, ct );
    }

    public async Task<Property> UpdateAsync( UpdatePropertyServiceDto dto, CancellationToken ct = default )
    {
        Property? existing = await _propertyRepository.GetById( dto.Id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( "Property", dto.Id );
        }

        if ( string.IsNullOrWhiteSpace( dto.Name ) )
        {
            throw new ValidationDomainException( "Property name is required" );
        }

        existing.Name = dto.Name;
        existing.Country = dto.Country;
        existing.City = dto.City;
        existing.Address = dto.Address;
        existing.Latitude = dto.Latitude;
        existing.Longitude = dto.Longitude;

        return await _propertyRepository.Update( existing, ct );
    }

    public async Task DeleteAsync( Guid id, CancellationToken ct = default )
    {
        Property? existing = await _propertyRepository.GetById( id, ct );
        if ( existing == null )
        {
            throw new NotFoundException( "Property", id );
        }

        await _propertyRepository.Delete( existing, ct );
    }
}
