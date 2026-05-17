using CarFactory.Data;
using CarFactory.Services.Interfaces;
using CarFactory.Services.Implementations;
using CarFactory.Utils;

namespace CarFactory;

internal class Program
{
    private static void Main()
    {
        IDataProvider dataProvider = new GameData();
        IConsole console = new SystemConsole();
        IInputHelper inputHelper = new InputHelper( console );

        ICarCreator carFactory = new CarCreator( dataProvider, inputHelper );

        ICarManager carManager = new CarManager( carFactory, inputHelper, console );

        carManager.PlayGame();
    }
}