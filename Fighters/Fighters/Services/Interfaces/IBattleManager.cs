using Fighters.Models.Fighters;

namespace Fighters.Services.Interfaces
{
    public interface IBattleManager
    {
        void StartBattle( List<IFighter> fighters );
    }
}