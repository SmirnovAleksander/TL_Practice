using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public class GameManager : IGameManager
    {
        private readonly IFighterFactory _factory;
        private readonly IBattleManager _battleManager;

        public GameManager( IFighterFactory factory, IBattleManager battleManager )
        {
            _factory = factory;
            _battleManager = battleManager;
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine( "Fighters Game - Меню" );
            Console.WriteLine( "1 - Добавить бойца" );
            Console.WriteLine( "2 - Показать бойцов" );
            Console.WriteLine( "3 - Играть" );
            Console.WriteLine( "4 - Выйти" );
        }

        public void AddFighter( List<IFighter> fighters )
        {
            Console.WriteLine();
            Console.WriteLine( "Введите имя бойца:" );
            string name = Console.ReadLine() ?? "";

            if ( string.IsNullOrEmpty( name ) )
            {
                Console.WriteLine( "Имя не может быть пустым!" );
                return;
            }

            IFighter fighter = _factory.CreateFighter( name );
            fighters.Add( fighter );
            Console.WriteLine( "Боец добавлен!" );
        }

        public void ShowFighters( List<IFighter> fighters )
        {
            Console.WriteLine();

            if ( fighters.Count == 0 )
            {
                Console.WriteLine( "Нет бойцов!" );
                return;
            }

            Console.WriteLine( "Список бойцов:" );
            for ( int i = 0; i < fighters.Count; i++ )
            {
                IFighter f = fighters[ i ];
                Console.WriteLine( ( i + 1 ) + ". " + f.Name );
                Console.WriteLine( "   " + f.GetInfo() );
            }
        }

        public void Play( List<IFighter> fighters )
        {
            Console.WriteLine();

            if ( fighters.Count < 2 )
            {
                Console.WriteLine( "Для битвы нужно минимум 2 бойца!" );
                return;
            }

            _battleManager.StartBattle( fighters );
            fighters.Clear();
        }
    }
}