namespace CarFactory.Models.BodyForms
{
    public interface IBodyForm
    {
        public string Name { get; }
        public int DoorCount { get; }
        public double AirResistanceCoeff { get; }
        public int Weight { get; }
    }
}