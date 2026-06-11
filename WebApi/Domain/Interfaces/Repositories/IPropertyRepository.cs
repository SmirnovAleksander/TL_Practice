using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<IReadOnlyList<Property>> GetAll( CancellationToken ct = default );
    Task<Property?> GetById( Guid id, CancellationToken ct = default );
    Task<Property> Create( Property property, CancellationToken ct = default );
    Task<Property> Update( Property property, CancellationToken ct = default );
    Task Delete( Guid id, CancellationToken ct = default );
}
