namespace CarFactory.Models.GearBoxes;

public class Robot : IGearBox
{
    public string Name { get; } = "Робот";
    public int? GearCount { get; } = 7;
    public string TransmissionType { get; } = "robot";
    public double Coefficient { get; } = 1.08;
}