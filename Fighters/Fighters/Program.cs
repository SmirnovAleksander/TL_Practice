using Fighters.Data;
using Fighters.Services.Interfaces;
using Fighters.Services.Implementations;
using Fighters.Utils;

namespace Fighters;

internal class Program
{
    private static void Main()
    {
        IDataProvider dataProvider = new GameData();
        IConsole console = new SystemConsole();
        IInputHelper inputHelper = new InputHelper( console );
        IRandomProvider randomProvider = new RandomProvider();

        IFighterFactory factory = new FighterFactory( dataProvider, inputHelper, console );
        IDamageCalculator damageCalculator = new DamageCalculator( randomProvider );
        IBattleManager battleManager = new BattleManager( damageCalculator, console );
        IGameManager gameManager = new GameManager( factory, battleManager, inputHelper, console );

        gameManager.PlayGame();
    }
}