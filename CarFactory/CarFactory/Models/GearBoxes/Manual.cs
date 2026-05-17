using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class Manual : IGearBox
{
    public string Name { get; } = "Механика";
    public int? GearCount { get; } = 6;
    public TransmissionType TransmissionType { get; } = TransmissionType.Manual;
    public double CalculateGearFactor() => 1.1 * GearCount!.Value / 6.0;
}