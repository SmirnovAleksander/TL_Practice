using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class GasolineLarge : IEngine
{
    public string Name { get; } = "Бензин 3.0";
    public int Power { get; } = 280;
    public FuelType FuelType { get; } = FuelType.Gasoline;
}
