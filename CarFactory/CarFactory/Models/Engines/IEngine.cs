namespace CarFactory.Models.Engines;

public interface IEngine
{
    public string Name { get; }
    public int Power { get; }
    public string FuelType { get; }
}