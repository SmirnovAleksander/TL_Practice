namespace CarFactory.Models.Engines
{
    public class Diesel20 : IEngine
    {
        public string Name { get; } = "Дизель 2.0";
        public int Power { get; } = 150;
        public string FuelType { get; } = "diesel";
    }
}