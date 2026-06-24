using CarFactory.Data;
using CarFactory.Services.Interfaces;
using CarFactory.Services.Implementations;
using CarFactory.Utils;

namespace CarFactory;

internal class Program
{
    public static void Main()
    {
        IDataProvider dataProvider = new CarData();
        IConsole console = new SystemConsole();
        IInputHelper inputHelper = new InputHelper( console );

        var carFactory = new Services.Implementations.CarFactory( dataProvider, inputHelper );

        ICarManager carManager = new CarManager( carFactory, inputHelper, console );

        carManager.Run();
    }
}