using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public RoomTypeRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<List<RoomType>> GetByProperty( Guid propertyId, CancellationToken ct = default )
    {
        return await _dbContext.RoomTypes.Where( rt => rt.PropertyId == propertyId ).ToListAsync( ct );
    }

    public async Task<RoomType?> GetById( Guid id, CancellationToken ct = default )
    {
        return await _dbContext.RoomTypes.FindAsync( id, ct );
    }

    public async Task<RoomType> Create( RoomType roomType, CancellationToken ct = default )
    {
        await _dbContext.RoomTypes.AddAsync( roomType, ct );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task<RoomType> Update( RoomType roomType, CancellationToken ct = default )
    {
        _dbContext.RoomTypes.Update( roomType );
        await _dbContext.SaveChangesAsync( ct );

        return roomType;
    }

    public async Task Delete( Guid id, CancellationToken ct = default )
    {
        RoomType? roomType = await _dbContext.RoomTypes.FindAsync( id, ct );
        if ( roomType == null )
        {
            throw new InvalidOperationException( $"RoomType with id '{id}' not found" );
        }

        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync( ct );
    }
}
