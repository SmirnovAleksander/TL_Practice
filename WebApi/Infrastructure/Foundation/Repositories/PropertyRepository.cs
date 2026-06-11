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

    public async Task<IReadOnlyList<Property>> GetAll( CancellationToken ct = default )
    {
        return await _dbContext.Properties.ToListAsync( ct );
    }

    public async Task<Property?> GetById( Guid id, CancellationToken ct = default )
    {
        return await _dbContext.Properties.FindAsync( id, ct );
    }

    public async Task<Property> Create( Property property, CancellationToken ct = default )
    {
        await _dbContext.Properties.AddAsync( property, ct );
        await _dbContext.SaveChangesAsync( ct );

        return property;
    }

    public async Task<Property> Update( Property property, CancellationToken ct = default )
    {
        _dbContext.Properties.Update( property );
        await _dbContext.SaveChangesAsync( ct );

        return property;
    }

    public async Task Delete( Guid id, CancellationToken ct = default )
    {
        Property? property = await _dbContext.Properties.FindAsync( id, ct );
        if ( property == null )
        {
            throw new InvalidOperationException( $"Property with id '{id}' not found" );
        }

        _dbContext.Properties.Remove( property );
        await _dbContext.SaveChangesAsync( ct );
    }
}
