using CarFactory.Data;
using CarFactory.Models.BodyForms;
using CarFactory.Models.Cars;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.Interfaces;
using CarFactory.Models.SteeringWheelPositions;
using CarFactory.Services.Extensions;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class CarFactory : ICarFactory
{
    private readonly IDataProvider _dataProvider;
    private readonly IInputHelper _inputHelper;

    public CarFactory(
        IDataProvider dataProvider,
        IInputHelper inputHelper )
    {
        _dataProvider = dataProvider;
        _inputHelper = inputHelper;
    }

    public ICar CreateCar( string name )
    {
        IColor color = SelectItem( _dataProvider.Colors, "Выберите цвет:" );
        IBodyForm bodyForm = SelectItem( _dataProvider.BodyForms, "Выберите кузов:" );

        List<IEngine> availableEngines = _dataProvider.Engines.FilterByBodyForm( bodyForm );
        IEngine engine = SelectItem( availableEngines, "Выберите двигатель:" );

        List<IGearBox> availableGearBoxes = _dataProvider.GearBoxes.FilterByEngineAndBody( engine, bodyForm );
        IGearBox gearBox = SelectItem( availableGearBoxes, "Выберите коробку передач:" );

        ISteeringWheelPosition steeringWheelPosition = SelectItem( _dataProvider.SteeringWheelPositions, "Выберите положение руля:" );

        return new Car(
            name,
            color,
            bodyForm,
            engine,
            gearBox,
            steeringWheelPosition );
    }

    private T SelectItem<T>( List<T> items, string textMessage ) where T : INamed
    {
        Console.WriteLine( textMessage );
        for ( int i = 0; i < items.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {items[ i ].Name}" );
        }
        int choice = _inputHelper.ReadChoice( items.Count );
        return items[ choice - 1 ];
    }
}