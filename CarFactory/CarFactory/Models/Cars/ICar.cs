using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Models.Cars
{
    public interface ICar
    {
        string Name { get; }
        IColor Color { get; }
        IBodyForm BodyForm { get; }
        IEngine Engine { get; }
        IGearBox GearBox { get; }
        ISteeringWheelPosition SteeringWheelPosition { get; }

        int CalculateMaxSpeed();
        string GetInfo();
    }
}