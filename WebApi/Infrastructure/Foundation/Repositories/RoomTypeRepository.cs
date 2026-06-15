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
        CancellationToken ct,
        int? guests = null,
        decimal? maxPrice = null )
    {
        return await _dbContext.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .ApplyFilter( guests, maxPrice )
            .ToListAsync( ct );
    }

    public async Task<RoomType?> GetByIdAsync( Guid id, CancellationToken ct )
    {
        return await _dbContext.RoomTypes.FirstOrDefaultAsync( r => r.Id == id, ct );
    }

    public async Task<RoomType> CreateAsync( RoomType roomType, CancellationToken ct )
    {
        await _dbContext.RoomTypes.AddAsync( roomType, ct );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task<RoomType> UpdateAsync( RoomType roomType, CancellationToken ct )
    {
        _dbContext.RoomTypes.Update( roomType );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task DeleteAsync( RoomType roomType, CancellationToken ct )
    {
        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync( ct );
    }
}
