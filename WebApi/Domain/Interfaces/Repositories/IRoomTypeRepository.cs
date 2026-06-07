using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    List<RoomType> GetByProperty( Guid propertyId );
    RoomType? GetById( Guid id );
    RoomType Create( RoomType roomType );
    RoomType Update( RoomType roomType );
    void Delete( Guid id );
}
