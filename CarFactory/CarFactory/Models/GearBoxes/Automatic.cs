namespace CarFactory.Models.GearBoxes
{
    public class Automatic : IGearBox
    {
        public string Name { get; } = "Автомат";
        public int? GearCount { get; } = 6;
        public string TransmissionType { get; } = "automatic";
    }
}