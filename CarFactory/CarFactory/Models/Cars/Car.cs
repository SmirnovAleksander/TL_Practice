using CarFactory.Models.BodyForms;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;

namespace CarFactory.Models.Cars
{
    public class Car : ICar
    {
        private readonly IColor _color;
        private readonly IBodyForm _bodyForm;
        private readonly IEngine _engine;
        private readonly IGearBox _gearBox;
        private readonly ISteeringWheelPosition _steeringWheelPosition;

        public string Name { get; }
        public IColor Color => _color;
        public IBodyForm BodyForm => _bodyForm;
        public IEngine Engine => _engine;
        public IGearBox GearBox => _gearBox;
        public ISteeringWheelPosition SteeringWheelPosition => _steeringWheelPosition;

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
            double resistancePenalty = _bodyForm.AirResistanceCoeff * 20;
            int gearBonus = _gearBox.GearCount ?? 0 * 3;
            return ( int )( _engine.MaxSpeed + gearBonus - resistancePenalty );
        }

        public string GetInfo()
        {
            return "Цвет: " + _color.Name + ", Кузов: " + _bodyForm.Name
                + ", Двигатель: " + _engine.Name + ", КПП: " + _gearBox.Name
                + ", Руль: " + _steeringWheelPosition.Name + "\n"
                + "Макс. скорость: " + CalculateMaxSpeed() + " км/ч, Мощность: " + _engine.Power + " л.с.";
        }
    }
}