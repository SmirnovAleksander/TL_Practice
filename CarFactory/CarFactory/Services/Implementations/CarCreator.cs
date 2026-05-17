using CarFactory.Data;
using CarFactory.Models.BodyForms;
using CarFactory.Models.Cars;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;
using CarFactory.Services.Extensions;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class CarCreator : ICarCreator
{
    private readonly IDataProvider _dataProvider;
    private readonly IInputHelper _inputHelper;

    public CarCreator(
        IDataProvider dataProvider,
        IInputHelper inputHelper )
    {
        _dataProvider = dataProvider;
        _inputHelper = inputHelper;
    }

    public ICar CreateCar( string name )
    {
        IColor color = _inputHelper.SelectItem( _dataProvider.Colors, c => c.Name, "Выберите цвет:" );
        IBodyForm bodyForm = _inputHelper.SelectItem( _dataProvider.BodyForms, b => b.Name, "Выберите кузов:" );

        List<IEngine> availableEngines = _dataProvider.Engines.FilterByBodyForm( bodyForm );
        IEngine engine = _inputHelper.SelectItem( availableEngines, e => e.Name, "Выберите двигатель:" );

        List<IGearBox> availableGearBoxes = _dataProvider.GearBoxes.FilterByEngineAndBody( engine, bodyForm );
        IGearBox gearBox = _inputHelper.SelectItem( availableGearBoxes, g => g.Name, "Выберите коробку передач:" );

        ISteeringWheelPosition steeringWheelPosition = _inputHelper.SelectItem( _dataProvider.SteeringWheelPositions, s => s.Name, "Выберите положение руля:" );

        return new Car( name, color, bodyForm, engine, gearBox, steeringWheelPosition );
    }
}