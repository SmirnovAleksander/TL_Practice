using Fighters.Models.Fighters;
using Fighters.Services.Interfaces;

namespace Fighters.Services.Implementations
{
    public class DamageCalculator : IDamageCalculator
    {
        private const int MinRandomModifier = -20;
        private const int MaxRandomModifier = 10;
        private const double CriticalChance = 0.15;
        private const int CriticalMultiplier = 2;

        private readonly Random _random = new();

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