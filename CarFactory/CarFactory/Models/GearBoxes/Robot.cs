using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class Robot : IGearBox
{
    public string Name { get; } = "Робот";
    public int? GearCount { get; } = 7;
    public TransmissionType TransmissionType { get; } = TransmissionType.Robot;
    public double Coefficient { get; } = 1.2;
}