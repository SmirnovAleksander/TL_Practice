using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class Benzine16 : IEngine
{
    public string Name { get; } = "Бензин 1.6";
    public int Power { get; } = 130;
    public FuelType FuelType { get; } = FuelType.Benzine;
}