using CarFactory.Models.Cars;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class CarManager : ICarManager
{
    private readonly ICarFactory _carFactory;
    private readonly IInputHelper _inputHelper;
    private readonly IConsole _console;
    private readonly List<ICar> _cars = [];

    public CarManager(
        ICarFactory carFactory,
        IInputHelper inputHelper,
        IConsole console )
    {
        _carFactory = carFactory;
        _inputHelper = inputHelper;
        _console = console;
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

        _console.WriteLine();
        _console.WriteLine( "Car Factory - Меню" );
        for ( int i = 0; i < menuItems.Count; i++ )
        {
            _console.WriteLine( $"{i + 1} - {menuItems[ i ]}" );
        }
    }

    private void CreateCar()
    {
        _console.WriteLine();
        _console.WriteLine( "Введите название машины:" );
        string name = _console.ReadLine();

        if ( string.IsNullOrEmpty( name ) )
        {
            _console.WriteLine( "Название не может быть пустым!" );

            return;
        }

        ICar car = _carFactory.CreateCar( name );
        _cars.Add( car );
        _console.WriteLine( "Машина создана!" );
    }

    private void ShowCars()
    {
        _console.WriteLine();

        if ( _cars.Count == 0 )
        {
            _console.WriteLine( "Нет машин!" );

            return;
        }

        _console.WriteLine( "Список машин:" );
        for ( int i = 0; i < _cars.Count; i++ )
        {
            ICar car = _cars[ i ];
            _console.WriteLine( $"{i + 1}. Имя: {car.Name}" );
            _console.WriteLine( car.ToString() );
        }
    }

}