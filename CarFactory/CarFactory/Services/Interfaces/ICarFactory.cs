using CarFactory.Models.Cars;

namespace CarFactory.Services.Interfaces;

public interface ICarFactory
{
    ICar CreateCar( string name );
}