using Fighters.Models.Fighters;

namespace Fighters.Services.Interfaces
{
    public interface IFighterFactory
    {
        IFighter CreateFighter( string name );
    }
}