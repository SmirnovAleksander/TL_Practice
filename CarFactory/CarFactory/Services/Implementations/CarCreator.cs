using CarFactory.Models.BodyForms;
using CarFactory.Models.Cars;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class CarCreator : ICarCreator
{
    private readonly List<IColor> _colors;
    private readonly List<IBodyForm> _bodyForms;
    private readonly List<IEngine> _engines;
    private readonly List<IGearBox> _gearBoxes;
    private readonly List<ISteeringWheelPosition> _steeringWheelPositions;
    private readonly ICarOptionFilter _optionFilter;

    public CarCreator(
        List<IColor> colors,
        List<IBodyForm> bodyForms,
        List<IEngine> engines,
        List<IGearBox> gearBoxes,
        List<ISteeringWheelPosition> steeringWheelPositions,
        ICarOptionFilter optionFilter )
    {
        _colors = colors;
        _bodyForms = bodyForms;
        _engines = engines;
        _gearBoxes = gearBoxes;
        _steeringWheelPositions = steeringWheelPositions;
        _optionFilter = optionFilter;
    }

    public ICar CreateCar( string name )
    {
        Console.WriteLine( "Выберите цвет:" );
        InputHelper.DisplayOptions( _colors.ConvertAll( c => c.Name ) );
        IColor color = _colors[ InputHelper.ReadChoice( _colors.Count ) ];

        Console.WriteLine( "Выберите кузов:" );
        InputHelper.DisplayOptions( _bodyForms.ConvertAll( b => b.Name ) );
        IBodyForm bodyForm = _bodyForms[ InputHelper.ReadChoice( _bodyForms.Count ) ];

        List<IEngine> availableEngines = _optionFilter.FilterEngines( bodyForm, _engines );

        Console.WriteLine( "Выберите двигатель:" );
        InputHelper.DisplayOptions( availableEngines.ConvertAll( e => e.Name ) );
        IEngine engine = availableEngines[ InputHelper.ReadChoice( availableEngines.Count ) ];

        List<IGearBox> availableGearBoxes = _optionFilter.FilterGearBoxes( engine, bodyForm, _gearBoxes );

        Console.WriteLine( "Выберите коробку передач:" );
        InputHelper.DisplayOptions( availableGearBoxes.ConvertAll( g => g.Name ) );
        IGearBox gearBox = availableGearBoxes[ InputHelper.ReadChoice( availableGearBoxes.Count ) ];

        Console.WriteLine( "Выберите положение руля:" );
        InputHelper.DisplayOptions( _steeringWheelPositions.ConvertAll( s => s.Name ) );
        ISteeringWheelPosition steeringWheelPosition = _steeringWheelPositions[ InputHelper.ReadChoice( _steeringWheelPositions.Count ) ];

        return new Car( name, color, bodyForm, engine, gearBox, steeringWheelPosition );

    }
}