using Fighters.Models.Fighters;

namespace Fighters.Services.Interfaces;

public interface IDamageCalculator
{
    int CalculateDamage( IFighter attacker, IFighter defender );
}