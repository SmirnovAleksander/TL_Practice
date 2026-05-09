using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Models.Cars;

public class Car : ICar
{
    private IColor _color;
    private IBodyForm _bodyForm;
    private IEngine _engine;
    private IGearBox _gearBox;
    private ISteeringWheelPosition _steeringWheelPosition;

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
        double weight = _bodyForm.Weight;
        double air = _bodyForm.AirResistanceCoeff;
        double gearCoeff = _gearBox.Coefficient;

        double gearFactor;
        if ( _gearBox.TransmissionType == "single" )
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

    public string GetInfo()
    {
        return "Цвет: " + _color.Name + ", Кузов: " + _bodyForm.Name
            + ", Двигатель: " + _engine.Name + ", КПП: " + _gearBox.Name
            + ", Руль: " + _steeringWheelPosition.Name + "\n"
            + "Макс. скорость: " + CalculateMaxSpeed() + " км/ч, Мощность: " + _engine.Power + " л.с.";
    }
}