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
        IInputHelper inputHelper = new InputHelper();

        ICarCreator carFactory = new CarCreator( dataProvider, inputHelper );

        ICarManager carManager = new CarManager( carFactory, inputHelper );

        carManager.PlayGame();
    }
}