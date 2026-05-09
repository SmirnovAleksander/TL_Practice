using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class Electric : IEngine
{
    public string Name { get; } = "Электро";
    public int Power { get; } = 220;
    public FuelType FuelType { get; } = FuelType.Electric;
}