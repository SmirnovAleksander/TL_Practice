using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
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
        double airResistanceCoeff = _bodyForm.AirResistanceCoeff;
        double gearFactor = _gearBox.GearCoefficient;
        const double speedFactor = 8;
        //такой формулы не существует в реальности, могу просто описать что она означает 
        //(мощность двигателя * коэфицент коробки передач * константа скорости)/(корень от массы кузова * аэродинамика кузова)
        //тоесть можность двигателя и КПП скорость увеличивают, а за счет массы и аэродинамики мы ее снижаем
        //speedFactor - это константа которая нужна чтобы сбалансировать формулу, иначе будет мало скорости 
        //при делении на корень массы я ее просто подбирал на рандом, сама эта константа ничего не значит
        double speed = ( power * gearFactor * speedFactor ) / ( Math.Sqrt( weight ) * airResistanceCoeff );

        return ( int )Math.Round( speed );
    }

    public override string ToString()
    {
        return $@"   Цвет: {_color.Name}, Кузов: {_bodyForm.Name}, Двигатель: {_engine.Name}, КПП: {_gearBox.Name}, Руль: {_steeringWheelPosition.Name}
   Макс. скорость: {CalculateMaxSpeed()} км/ч, Мощность: {_engine.Power} л.с., Количество передач: {_gearBox.GearCount}";
    }
}