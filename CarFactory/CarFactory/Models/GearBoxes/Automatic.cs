using CarFactory.Models.Enums;

namespace CarFactory.Models.GearBoxes;

public class Automatic : IGearBox
{
    public string Name { get; } = "Автомат";
    public int? GearCount { get; } = 6;
    public TransmissionType TransmissionType { get; } = TransmissionType.Automatic;
    public double Coefficient { get; } = 1.00;
}