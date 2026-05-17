namespace CarFactory.Models.BodyForms;

public class Coupe : IBodyForm
{
    public string Name { get; } = "Купе";
    public int DoorCount { get; } = 2;
    public double AirResistanceCoeff { get; } = 0.26;
    public int WeightKg { get; } = 1400;
}