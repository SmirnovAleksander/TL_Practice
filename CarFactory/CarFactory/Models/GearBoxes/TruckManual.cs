namespace CarFactory.Models.GearBoxes
{
    public class TruckManual : IGearBox
    {
        public string Name { get; } = "Механика пониженая + повышенная";
        public int? GearCount { get; } = 12;
        public string TransmissionType { get; } = "manual";
    }
}