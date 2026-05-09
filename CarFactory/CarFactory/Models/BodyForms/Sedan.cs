namespace CarFactory.Models.BodyForms;

public class Sedan : IBodyForm
{
    public string Name { get; } = "Седан";
    public int DoorCount { get; } = 4;
    public double AirResistanceCoeff { get; } = 0.3;
    public int Weight { get; } = 1500;
}