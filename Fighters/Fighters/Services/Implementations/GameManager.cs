using Fighters.Models.Fighters;
using Fighters.Services.Interfaces;
using Fighters.Utils;

namespace Fighters.Services.Implementations;

public class GameManager : IGameManager
{
    private readonly IFighterFactory _factory;
    private readonly IBattleManager _battleManager;
    private readonly List<IFighter> _fighters = [];

    public GameManager( IFighterFactory factory, IBattleManager battleManager )
    {
        _factory = factory;
        _battleManager = battleManager;
    }

    public void PlayGame()
    {
        const int maxOptionIndex = 4;

        while ( true )
        {
            ShowMenu();
            int choice = InputHelper.ReadChoice( maxOptionIndex );

            switch ( choice )
            {
                case 1:
                    AddFighter();
                    break;

                case 2:
                    ShowFighters();
                    break;

                case 3:
                    Play();
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
            "Добавить бойца",
            "Показать бойцов",
            "Играть",
            "Выйти"
        ];

        Console.WriteLine();
        Console.WriteLine( "Fighters Game - Меню" );
        for ( int i = 0; i < menuItems.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {menuItems[ i ]}" );
        }
    }

    private void AddFighter()
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
        _fighters.Add( fighter );
        Console.WriteLine( "Боец добавлен!" );
    }

    private void ShowFighters()
    {
        Console.WriteLine();

        if ( _fighters.Count == 0 )
        {
            Console.WriteLine( "Нет бойцов!" );

            return;
        }

        Console.WriteLine( "Список бойцов:" );
        for ( int i = 0; i < _fighters.Count; i++ )
        {
            IFighter f = _fighters[ i ];
            Console.WriteLine( ( i + 1 ) + ". " + f.Name );
            Console.WriteLine( f );
        }
    }

    private void Play()
    {
        Console.WriteLine();

        if ( _fighters.Count < 2 )
        {
            Console.WriteLine( "Для битвы нужно минимум 2 бойца!" );

            return;
        }

        _battleManager.StartBattle( _fighters );
        _fighters.Clear();
    }
}