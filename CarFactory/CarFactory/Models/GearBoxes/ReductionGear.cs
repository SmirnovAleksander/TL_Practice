using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class ReductionGear : IGearBox
{
    public string Name { get; } = "Редуктор";
    public int? GearCount { get; } = 1;
    public TransmissionType TransmissionType { get; } = TransmissionType.ReductionGear;
    public double CalculateGearFactor() => 1.0;
}