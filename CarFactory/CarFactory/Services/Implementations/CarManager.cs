using CarFactory.Models.Cars;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class CarManager : ICarManager
{
    private readonly ICarCreator _carFactory;
    private readonly IInputHelper _inputHelper;
    private readonly List<ICar> _cars = [];

    public CarManager( ICarCreator carFactory, IInputHelper inputHelper )
    {
        _carFactory = carFactory;
        _inputHelper = inputHelper;
    }

    public void PlayGame()
    {
        const int maxMenuOption = 3;
        bool isRunning = true;

        while ( isRunning )
        {
            ShowMenu();
            int choice = _inputHelper.ReadChoice( maxMenuOption );

            switch ( choice )
            {
                case 1:
                    CreateCar();
                    break;

                case 2:
                    ShowCars();
                    break;

                case 3:
                    isRunning = false;
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        List<string> menuItems =
        [
            "Создать машину",
            "Показать машины",
            "Выйти"
        ];

        Console.WriteLine();
        Console.WriteLine( "Car Factory - Меню" );
        for ( int i = 0; i < menuItems.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {menuItems[ i ]}" );
        }
    }

    private void CreateCar()
    {
        Console.WriteLine();
        Console.WriteLine( "Введите название машины:" );
        string name = Console.ReadLine() ?? "";

        if ( string.IsNullOrEmpty( name ) )
        {
            Console.WriteLine( "Название не может быть пустым!" );

            return;
        }

        ICar car = _carFactory.CreateCar( name );
        _cars.Add( car );
        Console.WriteLine( "Машина создана!" );
    }

    private void ShowCars()
    {
        Console.WriteLine();

        if ( _cars.Count == 0 )
        {
            Console.WriteLine( "Нет машин!" );

            return;
        }

        Console.WriteLine( "Список машин:" );
        for ( int i = 0; i < _cars.Count; i++ )
        {
            ICar car = _cars[ i ];
            Console.WriteLine( $"{i + 1}. Имя: {car.Name}" );
            Console.WriteLine( car );
        }
    }

}