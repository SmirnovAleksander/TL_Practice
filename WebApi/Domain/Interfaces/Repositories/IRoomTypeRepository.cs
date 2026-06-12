using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<IReadOnlyList<RoomType>> GetByProperty( Guid propertyId, CancellationToken ct = default );
    Task<RoomType?> GetById( Guid id, CancellationToken ct = default );
    Task<RoomType> Create( RoomType roomType, CancellationToken ct = default );
    Task<RoomType> Update( RoomType roomType, CancellationToken ct = default );
    Task Delete( RoomType roomType, CancellationToken ct = default );
}
