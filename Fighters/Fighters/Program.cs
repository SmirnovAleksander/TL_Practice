using Fighters.Data;
using Fighters.Services.Interfaces;
using Fighters.Services.Implementations;

namespace Fighters;

internal class Program
{
    private static void Main()
    {
        IRandomProvider randomProvider = new RandomProvider();
        
        IFighterFactory factory = new FighterFactory( 
            GameData.Races, 
            GameData.Weapons, 
            GameData.Armors, 
            GameData.Classes );

        IDamageCalculator damageCalculator = new DamageCalculator( randomProvider );
        IBattleManager battleManager = new BattleManager( damageCalculator );
        IGameManager gameManager = new GameManager( factory, battleManager );

        gameManager.PlayGame();
    }
}