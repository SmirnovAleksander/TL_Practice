using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Data;

public interface IDataProvider
{
    List<IColor> Colors { get; }
    List<IBodyForm> BodyForms { get; }
    List<IEngine> Engines { get; }
    List<IGearBox> GearBoxes { get; }
    List<ISteeringWheelPosition> SteeringWheelPositions { get; }
}
