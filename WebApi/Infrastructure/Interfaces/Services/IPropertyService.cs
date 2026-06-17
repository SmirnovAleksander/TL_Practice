using Infrastructure.Dto.Properties;
using Domain.Entities;

namespace Infrastructure.Interfaces.Services;

public interface IPropertyService
{
    Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct );

    Task<Property> GetByIdAsync( Guid id, CancellationToken ct );

    Task<Property> CreateAsync( CreatePropertyDto dto, CancellationToken ct );

    Task<Property> UpdateAsync( UpdatePropertyDto dto, CancellationToken ct );

    Task DeleteAsync( Guid id, CancellationToken ct );
}
