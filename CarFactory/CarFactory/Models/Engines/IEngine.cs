namespace CarFactory.Models.Engines
{
    public interface IEngine
    {
        public string Name { get; }
        public int Power { get; }
        public int MaxSpeed { get; }
        public string FuelType { get; }
    }
}