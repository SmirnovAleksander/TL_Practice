using Fighters.Models.Fighters;
using Fighters.Services.Interfaces;
using Fighters.Utils;

namespace Fighters.Services.Implementations;

public class BattleManager : IBattleManager
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly IConsole _console;

    public BattleManager( IDamageCalculator damageCalculator, IConsole console )
    {
        _damageCalculator = damageCalculator;
        _console = console;
    }

    public void StartBattle( List<IFighter> fighters )
    {
        if ( fighters.Count < 2 )
        {
            _console.WriteLine( "Для битвы нужно минимум 2 бойца!" );

            return;
        }

        List<IFighter> orderedFighters = fighters.OrderByDescending( f => f.Initiative ).ToList();
        int round = 1;

        while ( orderedFighters.Count > 1 )
        {
            _console.WriteLine( "" );
            _console.WriteLine( $"Раунд {round}" );
            RoundFight( orderedFighters );
            orderedFighters.RemoveAll( f => !f.IsAlive() );
            round++;
        }

        _console.WriteLine( $"{orderedFighters[ 0 ].Name} выигрывает битву!" );
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

            _console.WriteLine( $"{attacker.Name} наносит {damage} урона {defender.Name}" );
            _console.WriteLine( $"{defender.Name} получил {damage} урона, осталось {defender.GetCurrentHealth()} HP" );
        }
    }
}