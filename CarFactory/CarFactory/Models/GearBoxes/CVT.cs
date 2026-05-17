using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class CVT : IGearBox
{
    public string Name { get; } = "Вариатор";
    public int? GearCount { get; } = null;
    public TransmissionType TransmissionType { get; } = TransmissionType.Cvt;
    public double CalculateGearFactor() => 0.9;
}