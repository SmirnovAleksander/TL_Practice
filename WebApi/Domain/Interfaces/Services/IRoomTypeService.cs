using Domain.Dtos.RoomType;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IRoomTypeService
{
    Task<List<RoomType>> GetByPropertyAsync( Guid propertyId, CancellationToken ct = default );
    Task<RoomType> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<RoomType> CreateAsync( CreateRoomTypeServiceDto dto, CancellationToken ct = default );
    Task<RoomType> UpdateAsync( UpdateRoomTypeServiceDto dto, CancellationToken ct = default );
    Task DeleteAsync( Guid id, CancellationToken ct = default );
}
