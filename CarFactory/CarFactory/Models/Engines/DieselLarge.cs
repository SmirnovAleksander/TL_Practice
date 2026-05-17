using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class DieselLarge : IEngine
{
    public string Name { get; } = "Дизель 4.0";
    public int Power { get; } = 320;
    public FuelType FuelType { get; } = FuelType.Diesel;
}
