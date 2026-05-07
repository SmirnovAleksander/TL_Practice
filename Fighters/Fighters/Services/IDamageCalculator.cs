using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public interface IDamageCalculator
    {
        int CalculateDamage( IFighter attacker, IFighter defender );
    }
}