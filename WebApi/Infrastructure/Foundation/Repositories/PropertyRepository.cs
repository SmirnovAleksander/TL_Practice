using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Data;

namespace Infrastructure.Foundation.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly HotelManagementDbContext _dbContext;
    public PropertyRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public List<Property> GetAll()
    {
        return _dbContext.Properties.ToList();
    }

    public Property? GetById(Guid id)
    {
        return _dbContext.Properties.Find(id);
    }

    public Property Create(Property property)
    {
        property.Id = Guid.NewGuid();
        _dbContext.Properties.Add(property);
        _dbContext.SaveChanges();

        return property;
    }

    public Property Update(Property property)
    {
        Property existing = _dbContext.Properties.Find(property.Id)!;

        existing.Name = property.Name;
        existing.Country = property.Country;
        existing.City = property.City;
        existing.Address = property.Address;
        existing.Latitude = property.Latitude;
        existing.Longitude = property.Longitude;

        _dbContext.SaveChanges();
        
        return existing;
    }

    public void Delete(Guid id)
    {
        Property? property = _dbContext.Properties.Find(id);
        if (property == null)
        {
            return;
        }
        
        _dbContext.Properties.Remove(property);
        _dbContext.SaveChanges();
    }
}
