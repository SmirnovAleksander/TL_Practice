namespace CarFactory.Models.BodyForms
{
    public class Pickup : IBodyForm
    {
        public string Name { get; } = "Пикап";
        public int DoorCount { get; } = 4;
        public double AirResistanceCoeff { get; } = 0.5;
        public int Weight { get; } = 2500;
    }
}