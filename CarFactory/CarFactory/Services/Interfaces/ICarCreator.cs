using CarFactory.Models.Cars;

namespace CarFactory.Services.Interfaces;

public interface ICarCreator
{
    ICar CreateCar( string name );
}