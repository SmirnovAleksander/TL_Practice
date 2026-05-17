using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class CVT : IGearBox
{
    public string Name { get; } = "Вариатор";
    public int GearCount { get; } = 0;
    public TransmissionType TransmissionType { get; } = TransmissionType.Cvt;
    public double GearCoefficient { get; } = 0.9;
}