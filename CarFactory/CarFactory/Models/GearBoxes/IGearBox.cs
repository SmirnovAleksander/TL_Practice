using CarFactory.Models.Enums;
using CarFactory.Models.Interfaces;

namespace CarFactory.Models.GearBoxes;

public interface IGearBox : INamed
{
    public int GearCount { get; }
    public TransmissionType TransmissionType { get; }
    public double GearCoefficient { get; }
}