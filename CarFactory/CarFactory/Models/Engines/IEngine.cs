using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public interface IEngine
{
    public string Name { get; }
    public int Power { get; }
    public FuelType FuelType { get; }
}