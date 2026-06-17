using Infrastructure.Dto.RoomTypes;
using Domain.Entities;

namespace Infrastructure.Interfaces.Services;

public interface IRoomTypeService
{
    Task<IReadOnlyList<RoomType>> GetByPropertyAsync( Guid propertyId, CancellationToken ct );

    Task<RoomType> GetByIdAsync( Guid id, CancellationToken ct );

    Task<RoomType> CreateAsync( CreateRoomTypeDto dto, CancellationToken ct );

    Task<RoomType> UpdateAsync( UpdateRoomTypeDto dto, CancellationToken ct );

    Task DeleteAsync( Guid id, CancellationToken ct );
}
