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
        IInputHelper inputHelper = new InputHelper();
        IRandomProvider randomProvider = new RandomProvider();

        IFighterFactory factory = new FighterFactory( dataProvider, inputHelper );
        IDamageCalculator damageCalculator = new DamageCalculator( randomProvider );
        IBattleManager battleManager = new BattleManager( damageCalculator );
        IGameManager gameManager = new GameManager( factory, battleManager, inputHelper );

        gameManager.PlayGame();
    }
}