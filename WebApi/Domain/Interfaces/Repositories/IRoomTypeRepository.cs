using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<List<RoomType>> GetByProperty( Guid propertyId );
    Task<RoomType?> GetById( Guid id );
    Task<RoomType> Create( RoomType roomType );
    Task<RoomType> Update( RoomType roomType );
    Task Delete( Guid id );
}
