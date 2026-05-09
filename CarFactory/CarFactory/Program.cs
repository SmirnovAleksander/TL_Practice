using CarFactory.Models.BodyForms;
using CarFactory.Models.Cars;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.GearBoxes;
using CarFactory.Models.SteeringWheelPositions;
using CarFactory.Services.Interfaces;
using CarFactory.Services.Implementations;
using CarFactory.Utils;

namespace CarFactory;

internal class Program
{
    static void Main()
    {
        List<IColor> colors = new List<IColor>
        {
            new Red(),
            new Blue(),
            new Black(),
            new White()
        };

        List<IBodyForm> bodyForms = new List<IBodyForm>
        {
            new Sedan(),
            new Hatchback(),
            new Pickup(),
            new Coupe()
        };

        List<IEngine> engines = new List<IEngine>
        {
            new Benzine16(),
            new Benzine30(),
            new Diesel20(),
            new Diesel40(),
            new Electric()
        };

        List<IGearBox> gearBoxes = new List<IGearBox>
        {
            new Manual(),
            new Automatic(),
            new Robot(),
            new CVT(),
            new SingleSpeed()
        };

        List<ISteeringWheelPosition> steeringWheelPositions = new List<ISteeringWheelPosition>
        {
            new RightWheel(),
            new LeftWheel()
        };

        ICarOptionFilter optionFilter = new CarOptionFilter();

        ICarCreator carFactory = new CarCreator( colors, bodyForms, engines, gearBoxes, steeringWheelPositions, optionFilter );
        IGameManager gameManager = new GameManager( carFactory );

        List<ICar> cars = new List<ICar>();

        const int MenuExit = 5;

        while ( true )
        {
            gameManager.ShowMenu();
            int choice = InputHelper.ReadChoice( MenuExit );

            switch ( choice )
            {
                case 1:
                    gameManager.CreateCar( cars );
                    break;

                case 2:
                    gameManager.ShowCars( cars );
                    break;

                case 3:
                    gameManager.CompareCars( cars );
                    break;

                case 4:
                    gameManager.RaceAll( cars );
                    break;

                case 5:
                    return;
            }
        }
    }
}