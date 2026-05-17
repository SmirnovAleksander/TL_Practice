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
        IColor color = InputHelper.SelectItem( _colors, c => c.Name, "Выберите цвет:" );
        IBodyForm bodyForm = InputHelper.SelectItem( _bodyForms, b => b.Name, "Выберите кузов:" );

        List<IEngine> availableEngines = _optionFilter.FilterEngines( bodyForm, _engines );
        IEngine engine = InputHelper.SelectItem( availableEngines, e => e.Name, "Выберите двигатель:" );

        List<IGearBox> availableGearBoxes = _optionFilter.FilterGearBoxes( engine, bodyForm, _gearBoxes );
        IGearBox gearBox = InputHelper.SelectItem( availableGearBoxes, g => g.Name, "Выберите коробку передач:" );

        ISteeringWheelPosition steeringWheelPosition = InputHelper.SelectItem( _steeringWheelPositions, s => s.Name, "Выберите положение руля:" );

        return new Car( name, color, bodyForm, engine, gearBox, steeringWheelPosition );
    }
}