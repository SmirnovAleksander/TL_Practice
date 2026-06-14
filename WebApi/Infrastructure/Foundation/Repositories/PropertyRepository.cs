using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Infrastructure.Foundation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public PropertyRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync( string? city = null, CancellationToken ct = default )
    {
        IQueryable<Property> query = _dbContext.Properties.AsQueryable().PropertyFilter( city );

        return await query.ToListAsync( ct );
    }

    public async Task<Property?> GetByIdAsync( Guid id, CancellationToken ct = default )
    {
        return await _dbContext.Properties.FindAsync( id, ct );
    }

    public async Task<Property> CreateAsync( Property property, CancellationToken ct = default )
    {
        await _dbContext.Properties.AddAsync( property, ct );
        await _dbContext.SaveChangesAsync( ct );

        return property;
    }

    public async Task<Property> UpdateAsync( Property property, CancellationToken ct = default )
    {
        _dbContext.Properties.Update( property );
        await _dbContext.SaveChangesAsync( ct );

        return property;
    }

    public async Task DeleteAsync( Property property, CancellationToken ct = default )
    {
        _dbContext.Properties.Remove( property );
        await _dbContext.SaveChangesAsync( ct );
    }
}
