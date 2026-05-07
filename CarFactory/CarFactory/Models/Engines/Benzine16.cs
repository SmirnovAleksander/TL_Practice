namespace CarFactory.Models.Engines
{
    public class Benzine16 : IEngine
    {
        public string Name { get; } = "Бензин 1.6";
        public int Power { get; } = 130;
        public int MaxSpeed { get; } = 200;
        public string FuelType { get; } = "benzine";
    }
}