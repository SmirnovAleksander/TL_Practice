using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public class DamageCalculator
    {
        Random _random = new();
        const int MinRandomModifier = -20;
        const int MaxRandomModifier = 10;
        const double CriticalChance = 0.15;
        const int CriticalMultiplier = 2;

        public int CalculateDamage( IFighter attacker, IFighter defender )
        {
            int baseDamage = attacker.CalculateDamage();
            int defenderArmor = defender.CalculateArmor();

            int randomModifier = _random.Next( MinRandomModifier, MaxRandomModifier + 1 );
            double modifier = 1.0 + ( randomModifier / 100.0 );
            int newDamage = ( int )( baseDamage * modifier );

            bool isCritical = _random.NextDouble() < CriticalChance;
            if ( isCritical )
            {
                newDamage *= CriticalMultiplier;
            }

            int finalDamage = newDamage - defenderArmor;

            if ( finalDamage < 0 )
            {
                finalDamage = 0;
            }

            return finalDamage;
        }
    }
}