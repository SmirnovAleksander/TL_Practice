using CarFactory.Models.Enums;

namespace CarFactory.Models.Engines;

public class DieselMedium : IEngine
{
    public string Name { get; } = "Дизель 2.0";
    public int Power { get; } = 150;
    public FuelType FuelType { get; } = FuelType.Diesel;
}
