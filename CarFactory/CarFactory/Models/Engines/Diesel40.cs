namespace CarFactory.Models.Engines
{
    public class Diesel40 : IEngine
    {
        public string Name { get; } = "Дизель 4.0";
        public int Power { get; } = 320;
        public int MaxSpeed { get; } = 230;
        public string FuelType { get; } = "diesel";
    }
}