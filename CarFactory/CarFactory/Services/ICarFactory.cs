using CarFactory.Models.Cars;

namespace CarFactory.Services
{
    public interface ICarFactory
    {
        ICar CreateCar( string name );
    }
}