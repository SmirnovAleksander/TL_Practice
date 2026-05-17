using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Data;

public class CarData : IDataProvider
{
    public List<IColor> Colors { get; } =
    [
        new Red(),
        new Blue(),
        new Black(),
        new White()
    ];

    public List<IBodyForm> BodyForms { get; } =
    [
        new Sedan(),
        new Hatchback(),
        new Pickup(),
        new Coupe()
    ];

    public List<IEngine> Engines { get; } =
    [
        new GasolineSmall(),
        new GasolineLarge(),
        new DieselMedium(),
        new DieselLarge(),
        new Electric()
    ];

    public List<IGearBox> GearBoxes { get; } =
    [
        new Manual(),
        new Automatic(),
        new Robot(),
        new CVT(),
        new ReductionGear()
    ];

    public List<ISteeringWheelPosition> SteeringWheelPositions { get; } =
    [
        new RightWheel(),
        new LeftWheel()
    ];
}
