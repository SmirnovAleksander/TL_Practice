using CarFactory.Models.BodyForms;
using CarFactory.Models.Cars;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;
using CarFactory.Utils;

namespace CarFactory.Services
{
    public class CarCreator : ICarFactory
    {
        private readonly List<IColor> _colors;
        private readonly List<IBodyForm> _bodyForms;
        private readonly List<IEngine> _engines;
        private readonly List<IGearBox> _gearBoxes;
        private readonly List<ISteeringWheelPosition> _steeringWheelPositions;

        public CarCreator(
            List<IColor> colors,
            List<IBodyForm> bodyForms,
            List<IEngine> engines,
            List<IGearBox> gearBoxes,
            List<ISteeringWheelPosition> steeringWheelPositions )
        {
            _colors = colors;
            _bodyForms = bodyForms;
            _engines = engines;
            _gearBoxes = gearBoxes;
            _steeringWheelPositions = steeringWheelPositions;
        }

        public ICar CreateCar( string name )
        {
            Console.WriteLine( "Выберите цвет:" );
            InputHelper.DisplayOptions( _colors.ConvertAll( c => c.Name ) );
            int colorIndex = InputHelper.ReadChoice( _colors.Count );

            Console.WriteLine( "Выберите кузов:" );
            InputHelper.DisplayOptions( _bodyForms.ConvertAll( b => b.Name ) );
            int bodyFormIndex = InputHelper.ReadChoice( _bodyForms.Count );

            Console.WriteLine( "Выберите двигатель:" );
            InputHelper.DisplayOptions( _engines.ConvertAll( e => e.Name ) );
            int engineIndex = InputHelper.ReadChoice( _engines.Count );

            Console.WriteLine( "Выберите коробку передач:" );
            InputHelper.DisplayOptions( _gearBoxes.ConvertAll( g => g.Name ) );
            int gearBoxIndex = InputHelper.ReadChoice( _gearBoxes.Count );

            Console.WriteLine( "Выберите положение руля:" );
            InputHelper.DisplayOptions( _steeringWheelPositions.ConvertAll( s => s.Name ) );
            int steeringWheelIndex = InputHelper.ReadChoice( _steeringWheelPositions.Count );

            return new Car(
                name,
                _colors[ colorIndex ],
                _bodyForms[ bodyFormIndex ],
                _engines[ engineIndex ],
                _gearBoxes[ gearBoxIndex ],
                _steeringWheelPositions[ steeringWheelIndex ]
            );
        }
    }
}