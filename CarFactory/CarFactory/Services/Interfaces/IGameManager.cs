using CarFactory.Models.Cars;

namespace CarFactory.Services.Interfaces
{
    public interface IGameManager
    {
        void ShowMenu();
        void CreateCar( List<ICar> cars );
        void ShowCars( List<ICar> cars );
        void CompareCars( List<ICar> cars );
        void RaceAll( List<ICar> cars );
    }
}