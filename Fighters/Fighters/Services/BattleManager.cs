using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public class BattleManager
    {
        private DamageCalculator _damageCalculator = new();

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

            if ( orderedFighters.Count == 1 )
            {
                Console.WriteLine( $"{orderedFighters[ 0 ].Name} выигрывает битву!" );
            }
        }

        private void RoundFight( List<IFighter> fighters )
        {
            for ( int i = 0; i < fighters.Count; i++ )
            {
                IFighter attacker = fighters[ i ];
                if ( !attacker.IsAlive() ) continue;

                int nextIndex = i + 1;
                if ( nextIndex >= fighters.Count )
                {
                    nextIndex = 0;
                }

                IFighter defender = fighters[ nextIndex ];
                if ( !defender.IsAlive() ) continue;

                int damage = _damageCalculator.CalculateDamage( attacker, defender );
                defender.TakeDamage( damage );

                Console.WriteLine( $"{attacker.Name} наносит {damage} урона {defender.Name}" );
                Console.WriteLine( $"{defender.Name} получил {damage} урона, осталось {defender.GetCurrentHealth()} HP" );
            }
        }
    }
}