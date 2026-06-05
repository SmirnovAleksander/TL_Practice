namespace CarFactory.Models.BodyForms;

public class Hatchback : IBodyForm
{
    public string Name { get; } = "Хэтчбек";
    public int DoorCount { get; } = 5;
    public double AirResistanceCoeff { get; } = 0.35;
    public int WeightKg { get; } = 1300;
}