namespace CarFactory.Models.Engines
{
    public class Diesel80 : IEngine
    {
        public string Name { get; } = "Дизель 8.0";
        public int Power { get; } = 400;
        public int MaxSpeed { get; } = 220;
        public string FuelType { get; } = "diesel";
    }
}