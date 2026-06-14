using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<IReadOnlyList<Property>> GetAllAsync( string? city = null, CancellationToken ct = default );
    Task<Property?> GetByIdAsync( Guid id, CancellationToken ct = default );
    Task<Property> CreateAsync( Property property, CancellationToken ct = default );
    Task<Property> UpdateAsync( Property property, CancellationToken ct = default );
    Task DeleteAsync( Property property, CancellationToken ct = default );
}
