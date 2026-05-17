using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.Enums;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Models.Cars;

public class Car : ICar
{
    private readonly IColor _color;
    private readonly IBodyForm _bodyForm;
    private readonly IEngine _engine;
    private readonly IGearBox _gearBox;
    private readonly ISteeringWheelPosition _steeringWheelPosition;

    public string Name { get; }

    public Car(
        string name,
        IColor color,
        IBodyForm bodyForm,
        IEngine engine,
        IGearBox gearBox,
        ISteeringWheelPosition steeringWheelPosition )
    {
        Name = name;
        _color = color;
        _bodyForm = bodyForm;
        _engine = engine;
        _gearBox = gearBox;
        _steeringWheelPosition = steeringWheelPosition;
    }

    public int CalculateMaxSpeed()
    {
        double power = _engine.Power;
        double weight = _bodyForm.WeightKg;
        double air = _bodyForm.AirResistanceCoeff;
        double gearCoeff = _gearBox.Coefficient;

        double gearFactor;
        if ( _gearBox.TransmissionType == TransmissionType.Single )
        {
            gearFactor = 1.0;
        }
        else
        {
            gearFactor = ( _gearBox.GearCount ?? 6 ) / 6.0;
        }

        double speed = ( power * gearCoeff * gearFactor * 8 ) / ( Math.Sqrt( weight ) * air );

        return ( int )Math.Round( speed );
    }

    public override string ToString()
    {
        return $@"   Цвет: {_color.Name}, Кузов: {_bodyForm.Name}, Двигатель: {_engine.Name}, КПП: {_gearBox.Name}, Руль: {_steeringWheelPosition.Name}
   Макс. скорость: {CalculateMaxSpeed()} км/ч, Мощность: {_engine.Power} л.с.";
    }
}