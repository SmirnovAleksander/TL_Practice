namespace CarFactory.Models.GearBoxes
{
    public class Manual : IGearBox
    {
        public string Name { get; } = "Механика";
        public int? GearCount { get; } = 6;
        public string TransmissionType { get; } = "manual";
        public double Coefficient { get; } = 1.05;
    }
}