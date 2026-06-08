using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public PropertyRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<List<Property>> GetAll()
    {
        return await _dbContext.Properties.ToListAsync();
    }

    public async Task<Property?> GetById( Guid id )
    {
        return await _dbContext.Properties.FindAsync( id );
    }

    public async Task<Property> Create( Property property )
    {
        property.Id = Guid.NewGuid();
        _dbContext.Properties.Add( property );
        await _dbContext.SaveChangesAsync();

        return property;
    }

    public async Task<Property> Update( Property property )
    {
        Property? existing = await _dbContext.Properties.FindAsync( property.Id );
        if ( existing is null )
        {
            throw new InvalidOperationException( "Property not found" );
        }

        existing.Name = property.Name;
        existing.Country = property.Country;
        existing.City = property.City;
        existing.Address = property.Address;
        existing.Latitude = property.Latitude;
        existing.Longitude = property.Longitude;

        await _dbContext.SaveChangesAsync();

        return existing;
    }

    public async Task Delete( Guid id )
    {
        Property? property = await _dbContext.Properties.FindAsync( id );
        if ( property == null )
        {
            return;
        }

        _dbContext.Properties.Remove( property );
        await _dbContext.SaveChangesAsync();
    }
}
