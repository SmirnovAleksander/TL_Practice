using CarFactory.Models.Enums;
using CarFactory.Models.Interfaces;

namespace CarFactory.Models.Engines;

public interface IEngine : INamed
{
    public int Power { get; }
    public FuelType FuelType { get; }
}