using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public interface IFighterFactory
    {
        IFighter CreateFighter( string name );
    }
}