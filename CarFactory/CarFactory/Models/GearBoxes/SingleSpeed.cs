namespace CarFactory.Models.GearBoxes
{
    public class SingleSpeed : IGearBox
    {
        public string Name { get; } = "Редуктор";
        public int? GearCount { get; } = 1;
        public string TransmissionType { get; } = "single";
        public double Coefficient { get; } = 1.00;
    }
}