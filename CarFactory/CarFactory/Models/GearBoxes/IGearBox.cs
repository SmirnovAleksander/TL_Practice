namespace CarFactory.Models.GearBoxes
{
    public interface IGearBox
    {
        public string Name { get; }
        public int? GearCount { get; }
        public string TransmissionType { get; }
        public double Coefficient { get; }
    }
}