using CarFactory.Models.Cars;
using CarFactory.Services.Interfaces;
using CarFactory.Utils;

namespace CarFactory.Services.Implementations;

public class GameManager : IGameManager
{
    private readonly ICarCreator _carFactory;
    private readonly List<ICar> _cars = [];

    public GameManager( ICarCreator carFactory )
    {
        _carFactory = carFactory;
    }

    public void PlayGame()
    {
        const int maxMenuOption = 4;

        while ( true )
        {
            ShowMenu();
            int choice = InputHelper.ReadChoice( maxMenuOption );

            switch ( choice )
            {
                case 1:
                    CreateCar();
                    break;

                case 2:
                    ShowCars();
                    break;

                case 3:
                    CompareCars();
                    break;

                case 4:
                    return;
            }
        }
    }

    private void ShowMenu()
    {
        List<string> menuItems =
        [
            "Создать машину",
            "Показать машины",
            "Сравнить машины",
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

    private void CompareCars()
    {
        Console.WriteLine();

        if ( _cars.Count < 2 )
        {
            Console.WriteLine( "Нужно минимум 2 машины для сравнения!" );

            return;
        }

        Console.WriteLine( $"Выберите первую машину (1-{_cars.Count}):" );
        int index1 = InputHelper.ReadChoice( _cars.Count ) - 1;

        Console.WriteLine( $"Выберите вторую машину (1-{_cars.Count}):" );
        int index2 = InputHelper.ReadChoice( _cars.Count ) - 1;

        ICar car1 = _cars[ index1 ];
        ICar car2 = _cars[ index2 ];

        Console.WriteLine();
        Console.WriteLine( "=== Сравнение ===" );
        Console.WriteLine( $"{car1.Name}: макс. {car1.CalculateMaxSpeed()} км/ч" );
        Console.WriteLine( $"{car2.Name}: макс. {car2.CalculateMaxSpeed()} км/ч" );

        if ( car1.CalculateMaxSpeed() > car2.CalculateMaxSpeed() )
        {
            Console.WriteLine( $"Победитель: {car1.Name}" );
        }
        else if ( car2.CalculateMaxSpeed() > car1.CalculateMaxSpeed() )
        {
            Console.WriteLine( $"Победитель: {car2.Name}" );
        }
        else
        {
            Console.WriteLine( "Ничья!" );
        }
    }
}