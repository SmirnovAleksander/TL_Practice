using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public interface IGearBox
{
    public string Name { get; }
    public int? GearCount { get; }
    public TransmissionType TransmissionType { get; }
    public double CalculateGearFactor();
}