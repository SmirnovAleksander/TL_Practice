using Fighters.Models.Fighters;
using Fighters.Services.Interfaces;

namespace Fighters.Services.Implementations;

public class BattleManager : IBattleManager
{
    private readonly IDamageCalculator _damageCalculator;

    public BattleManager( IDamageCalculator damageCalculator )
    {
        _damageCalculator = damageCalculator;
    }

    public void StartBattle( List<IFighter> fighters )
    {
        if ( fighters.Count < 2 )
        {
            Console.WriteLine( "Для битвы нужно минимум 2 бойца!" );

            return;
        }

        List<IFighter> orderedFighters = fighters.OrderByDescending( f => f.Initiative ).ToList();
        int round = 1;

        while ( orderedFighters.Count > 1 )
        {
            Console.WriteLine();
            Console.WriteLine( $"Раунд {round}" );
            RoundFight( orderedFighters );
            orderedFighters.RemoveAll( f => !f.IsAlive() );
            round++;
        }

        Console.WriteLine( $"{orderedFighters[ 0 ].Name} выигрывает битву!" );
    }

    private void RoundFight( List<IFighter> fighters )
    {
        for ( int i = 0; i < fighters.Count; i++ )
        {
            IFighter attacker = fighters[ i ];
            int nextIndex = ( i + 1 ) % fighters.Count;

            IFighter defender = fighters[ nextIndex ];
            if ( !defender.IsAlive() )
            {
                continue;
            }

            int damage = _damageCalculator.CalculateDamage( attacker, defender );
            defender.TakeDamage( damage );

            Console.WriteLine( $"{attacker.Name} наносит {damage} урона {defender.Name}" );
            Console.WriteLine( $"{defender.Name} получил {damage} урона, осталось {defender.GetCurrentHealth()} HP" );
        }
    }
}