namespace CarFactory.Models.Engines;

public class Benzine30 : IEngine
{
    public string Name { get; } = "Бензин 3.0";
    public int Power { get; } = 280;
    public string FuelType { get; } = "benzine";
}