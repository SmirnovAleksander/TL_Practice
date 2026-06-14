using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<IReadOnlyList<RoomType>> GetByPropertyAsync( Guid propertyId, int? guests = null, decimal? maxPrice = null, CancellationToken ct = default );
    Task<RoomType?> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<RoomType> CreateAsync( RoomType roomType, CancellationToken ct = default );
    Task<RoomType> UpdateAsync( RoomType roomType, CancellationToken ct = default );
    Task DeleteAsync( RoomType roomType, CancellationToken ct = default );
}
