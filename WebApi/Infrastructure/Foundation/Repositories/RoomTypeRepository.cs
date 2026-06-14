using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Infrastructure.Foundation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public RoomTypeRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<RoomType>> GetByPropertyAsync(
        Guid propertyId,
        int? guests = null,
        decimal? maxPrice = null,
        CancellationToken ct = default )
    {
        IQueryable<RoomType> query = _dbContext.RoomTypes
            .Where( rt => rt.PropertyId == propertyId ).RoomTypeFilter( guests, maxPrice );

        return await query.ToListAsync( ct );
    }

    public async Task<RoomType?> GetByIdAsync( Guid id, CancellationToken ct = default )
    {
        return await _dbContext.RoomTypes.FindAsync( id, ct );
    }

    public async Task<RoomType> CreateAsync( RoomType roomType, CancellationToken ct = default )
    {
        await _dbContext.RoomTypes.AddAsync( roomType, ct );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task<RoomType> UpdateAsync( RoomType roomType, CancellationToken ct = default )
    {
        _dbContext.RoomTypes.Update( roomType );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task DeleteAsync( RoomType roomType, CancellationToken ct = default )
    {
        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync( ct );
    }
}
