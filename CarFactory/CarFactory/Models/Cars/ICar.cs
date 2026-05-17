namespace CarFactory.Models.Cars;

public interface ICar
{
    string Name { get; }
    int CalculateMaxSpeed();
}