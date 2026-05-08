namespace CarFactory.Models.GearBoxes
{
    public class CVT : IGearBox
    {
        public string Name { get; } = "Вариатор";
        public int? GearCount { get; } = null;
        public string TransmissionType { get; } = "cvt";
        public double Coefficient { get; } = 0.97;
    }
}