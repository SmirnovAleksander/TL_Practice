using CarFactory.Data;
using CarFactory.Services.Interfaces;
using CarFactory.Services.Implementations;

namespace CarFactory;

internal class Program
{
    private static void Main()
    {
        ICarOptionFilter optionFilter = new CarOptionFilter();

        ICarCreator carFactory = new CarCreator(
            GameData.Colors,
            GameData.BodyForms,
            GameData.Engines,
            GameData.GearBoxes,
            GameData.SteeringWheelPositions,
            optionFilter );

        IGameManager gameManager = new GameManager( carFactory );

        gameManager.PlayGame();
    }
}