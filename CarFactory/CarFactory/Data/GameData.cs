using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Data;

public static class GameData
{
    public static List<IColor> Colors { get; } =
    [
        new Red(),
        new Blue(),
        new Black(),
        new White()
    ];

    public static List<IBodyForm> BodyForms { get; } =
    [
        new Sedan(),
        new Hatchback(),
        new Pickup(),
        new Coupe()
    ];

    public static List<IEngine> Engines { get; } =
    [
        new GasolineSmall(),
        new GasolineLarge(),
        new DieselMedium(),
        new DieselLarge(),
        new Electric()
    ];

    public static List<IGearBox> GearBoxes { get; } =
    [
        new Manual(),
        new Automatic(),
        new Robot(),
        new CVT(),
        new SingleSpeed()
    ];

    public static List<ISteeringWheelPosition> SteeringWheelPositions { get; } =
    [
        new RightWheel(),
        new LeftWheel()
    ];
}
