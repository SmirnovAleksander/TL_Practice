using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<List<Property>> GetAll();
    Task<Property?> GetById( Guid id );
    Task<Property> Create( Property property );
    Task<Property> Update( Property property );
    Task Delete( Guid id );
}
