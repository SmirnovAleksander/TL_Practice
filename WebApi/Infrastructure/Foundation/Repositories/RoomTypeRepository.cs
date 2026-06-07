using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public RoomTypeRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public List<RoomType> GetByProperty( Guid propertyId )
    {
        return _dbContext.RoomTypes.Where( rt => rt.PropertyId == propertyId ).ToList();
    }

    public RoomType? GetById( Guid id )
    {
        return _dbContext.RoomTypes.Find( id );
    }

    public RoomType Create( RoomType roomType )
    {
        roomType.Id = Guid.NewGuid();
        _dbContext.RoomTypes.Add( roomType );
        _dbContext.SaveChanges();

        return roomType;
    }

    public RoomType Update( RoomType roomType )
    {
        RoomType existing = _dbContext.RoomTypes.Find( roomType.Id )!;

        existing.Name = roomType.Name;
        existing.DailyPrice = roomType.DailyPrice;
        existing.Currency = roomType.Currency;
        existing.MinPersonCount = roomType.MinPersonCount;
        existing.MaxPersonCount = roomType.MaxPersonCount;
        existing.Services = roomType.Services;
        existing.Amenities = roomType.Amenities;

        _dbContext.SaveChanges();

        return existing;
    }

    public void Delete( Guid id )
    {
        RoomType? roomType = _dbContext.RoomTypes.Find( id );
        if ( roomType == null )
        {
            return;
        }

        _dbContext.RoomTypes.Remove( roomType );
        _dbContext.SaveChanges();
    }
}
