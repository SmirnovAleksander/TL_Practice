namespace CarFactory.Models.BodyForms
{
    public class Truck : IBodyForm
    {
        public string Name { get; } = "Фура";
        public int DoorCount { get; } = 2;
        public double AirResistanceCoeff { get; } = 0.60;
        public int Weight { get; } = 9000;
    }
}