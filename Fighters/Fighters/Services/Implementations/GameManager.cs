using Fighters.Models.Fighters;
using Fighters.Services.Interfaces;
using Fighters.Utils;

namespace Fighters.Services.Implementations;

public class GameManager : IGameManager
{
    private readonly IFighterFactory _factory;
    private readonly IBattleManager _battleManager;
    private readonly IInputHelper _inputHelper;
    private readonly IConsole _console;
    private readonly List<IFighter> _fighters = [];

    public GameManager( IFighterFactory factory, IBattleManager battleManager, IInputHelper inputHelper, IConsole console )
    {
        _factory = factory;
        _battleManager = battleManager;
        _inputHelper = inputHelper;
        _console = console;
    }

    public void PlayGame()
    {
        const int maxOptionIndex = 4;
        bool isRunning = true;

        while ( isRunning )
        {
            ShowMenu();
            int choice = _inputHelper.ReadChoice( maxOptionIndex );

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
                    isRunning = false;
                    break;
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

        _console.WriteLine( "" );
        _console.WriteLine( "Fighters Game - Меню" );
        for ( int i = 0; i < menuItems.Count; i++ )
        {
            _console.WriteLine( $"{i + 1} - {menuItems[ i ]}" );
        }
    }

    private void AddFighter()
    {
        _console.WriteLine( "" );
        _console.WriteLine( "Введите имя бойца:" );
        string name = _console.ReadLine();

        if ( string.IsNullOrEmpty( name ) )
        {
            _console.WriteLine( "Имя не может быть пустым!" );

            return;
        }

        IFighter fighter = _factory.CreateFighter( name );
        _fighters.Add( fighter );
        _console.WriteLine( "Боец добавлен!" );
    }

    private void ShowFighters()
    {
        _console.WriteLine( "" );

        if ( _fighters.Count == 0 )
        {
            _console.WriteLine( "Нет бойцов!" );

            return;
        }

        _console.WriteLine( "Список бойцов:" );
        for ( int i = 0; i < _fighters.Count; i++ )
        {
            IFighter f = _fighters[ i ];
            _console.WriteLine( $"{i + 1}. Имя: {f.Name}" );
            _console.WriteLine( f );
        }
    }

    private void Play()
    {
        _console.WriteLine( "" );

        if ( _fighters.Count < 2 )
        {
            _console.WriteLine( "Для битвы нужно минимум 2 бойца!" );

            return;
        }

        _battleManager.StartBattle( _fighters );
        _fighters.Clear();
    }
}