using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    List<Property> GetAll();
    Property? GetById( Guid id );
    Property Create( Property property );
    Property Update( Property property );
    void Delete( Guid id );
}
