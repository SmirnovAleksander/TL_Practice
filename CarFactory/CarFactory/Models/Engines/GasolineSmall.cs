using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class GasolineSmall : IEngine
{
    public string Name { get; } = "Бензин 1.6";
    public int Power { get; } = 130;
    public FuelType FuelType { get; } = FuelType.Gasoline;
}
