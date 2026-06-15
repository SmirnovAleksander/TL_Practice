using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    Task<IReadOnlyList<RoomType>> GetByPropertyAsync(
        Guid propertyId,
        CancellationToken ct,
        int? guests = null,
        decimal? maxPrice = null );

    Task<RoomType?> GetByIdAsync( Guid id, CancellationToken ct );

    Task<RoomType> CreateAsync( RoomType roomType, CancellationToken ct );

    Task<RoomType> UpdateAsync( RoomType roomType, CancellationToken ct );

    Task DeleteAsync( RoomType roomType, CancellationToken ct );
}
