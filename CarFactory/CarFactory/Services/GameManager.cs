using CarFactory.Models.Cars;
using CarFactory.Utils;

namespace CarFactory.Services
{
    public class GameManager : IGameManager
    {
        private readonly ICarFactory _carFactory;

        public GameManager( ICarFactory carFactory )
        {
            _carFactory = carFactory;
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine( "Car Factory - Меню" );
            Console.WriteLine( "1 - Создать машину" );
            Console.WriteLine( "2 - Показать машины" );
            Console.WriteLine( "3 - Сравнить машины" );
            Console.WriteLine( "4 - Гонка всех" );
            Console.WriteLine( "5 - Выйти" );
        }

        public void CreateCar( List<ICar> cars )
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
            cars.Add( car );
            Console.WriteLine( "Машина создана!" );
        }

        public void ShowCars( List<ICar> cars )
        {
            Console.WriteLine();

            if ( cars.Count == 0 )
            {
                Console.WriteLine( "Нет машин!" );
                return;
            }

            Console.WriteLine( "Список машин:" );
            for ( int i = 0; i < cars.Count; i++ )
            {
                ICar car = cars[ i ];
                Console.WriteLine( ( i + 1 ) + ". " + car.Name );
                Console.WriteLine( "   " + car.GetInfo() );
            }
        }

        public void CompareCars( List<ICar> cars )
        {
            Console.WriteLine();

            if ( cars.Count < 2 )
            {
                Console.WriteLine( "Нужно минимум 2 машины для сравнения!" );
                return;
            }

            Console.WriteLine( "Выберите первую машину (0-" + ( cars.Count - 1 ) + "):" );
            int index1 = InputHelper.ReadChoice( cars.Count );

            Console.WriteLine( "Выберите вторую машину (0-" + ( cars.Count - 1 ) + "):" );
            int index2 = InputHelper.ReadChoice( cars.Count );

            ICar car1 = cars[ index1 ];
            ICar car2 = cars[ index2 ];

            Console.WriteLine();
            Console.WriteLine( "=== Сравнение ===" );
            Console.WriteLine( car1.Name + ": макс. " + car1.CalculateMaxSpeed() + " км/ч" );
            Console.WriteLine( car2.Name + ": макс. " + car2.CalculateMaxSpeed() + " км/ч" );

            if ( car1.CalculateMaxSpeed() > car2.CalculateMaxSpeed() )
            {
                Console.WriteLine( "Победитель: " + car1.Name );
            }
            else if ( car2.CalculateMaxSpeed() > car1.CalculateMaxSpeed() )
            {
                Console.WriteLine( "Победитель: " + car2.Name );
            }
            else
            {
                Console.WriteLine( "Ничья!" );
            }
        }

        public void RaceAll( List<ICar> cars )
        {
            Console.WriteLine();

            if ( cars.Count < 2 )
            {
                Console.WriteLine( "Нужно минимум 2 машины для гонки!" );
                return;
            }

            var sortedCars = cars.OrderByDescending( c => c.CalculateMaxSpeed() ).ToList();

            Console.WriteLine();
            Console.WriteLine( "=== Гонка всех ===" );
            for ( int i = 0; i < sortedCars.Count; i++ )
            {
                ICar car = sortedCars[ i ];
                Console.WriteLine( ( i + 1 ) + ". " + car.Name + " - " + car.CalculateMaxSpeed() + " км/ч" );
            }

            Console.WriteLine();
            Console.WriteLine( "Победитель: " + sortedCars[ 0 ].Name );
        }
    }
}