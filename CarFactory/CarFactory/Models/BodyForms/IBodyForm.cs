using CarFactory.Models.Interfaces;

namespace CarFactory.Models.BodyForms;

public interface IBodyForm : INamed
{
    public int DoorCount { get; }
    public double AirResistanceCoeff { get; }
    public int WeightKg { get; }
}