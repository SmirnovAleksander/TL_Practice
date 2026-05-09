using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class SingleSpeed : IGearBox
{
    public string Name { get; } = "Редуктор";
    public int? GearCount { get; } = 1;
    public TransmissionType TransmissionType { get; } = TransmissionType.Single;
    public double Coefficient { get; } = 1.00;
}