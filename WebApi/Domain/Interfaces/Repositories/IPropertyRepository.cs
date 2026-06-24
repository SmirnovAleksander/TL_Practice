using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct, string? city = null );

    Task<Property?> GetByIdAsync( Guid id, CancellationToken ct );

    Task<Property> CreateAsync( Property property, CancellationToken ct );

    Task<Property> UpdateAsync( Property property, CancellationToken ct );

    Task DeleteAsync( Property property, CancellationToken ct );
}
