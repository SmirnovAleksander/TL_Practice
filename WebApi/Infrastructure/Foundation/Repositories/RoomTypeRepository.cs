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

    public async Task<List<RoomType>> GetByProperty( Guid propertyId )
    {
        return await _dbContext.RoomTypes.Where( rt => rt.PropertyId == propertyId ).ToListAsync();
    }

    public async Task<RoomType?> GetById( Guid id )
    {
        return await _dbContext.RoomTypes.FindAsync( id );
    }

    public async Task<RoomType> Create( RoomType roomType )
    {
        roomType.Id = Guid.NewGuid();
        _dbContext.RoomTypes.Add( roomType );
        await _dbContext.SaveChangesAsync();

        return roomType;
    }

    public async Task<RoomType> Update( RoomType roomType )
    {
        RoomType? existing = await _dbContext.RoomTypes.FindAsync( roomType.Id );
        if ( existing is null )
        {
            throw new InvalidOperationException( "RoomType not found" );
        }

        existing.Name = roomType.Name;
        existing.DailyPrice = roomType.DailyPrice;
        existing.Currency = roomType.Currency;
        existing.MinPersonCount = roomType.MinPersonCount;
        existing.MaxPersonCount = roomType.MaxPersonCount;
        existing.Services = roomType.Services;
        existing.Amenities = roomType.Amenities;

        await _dbContext.SaveChangesAsync();

        return existing;
    }

    public async Task Delete( Guid id )
    {
        RoomType? roomType = await _dbContext.RoomTypes.FindAsync( id );
        if ( roomType == null )
        {
            return;
        }

        _dbContext.RoomTypes.Remove( roomType );
        await _dbContext.SaveChangesAsync();
    }
}
