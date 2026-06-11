using Domain.Dtos.Property;
using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IPropertyService
{
    Task<List<Property>> GetAllAsync( CancellationToken ct = default );
    Task<Property> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<Property> CreateAsync( CreatePropertyServiceDto dto, CancellationToken ct = default );
    Task<Property> UpdateAsync( UpdatePropertyServiceDto dto, CancellationToken ct = default );
    Task DeleteAsync( Guid id, CancellationToken ct = default );
}
