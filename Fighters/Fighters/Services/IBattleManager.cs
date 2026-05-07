using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public interface IBattleManager
    {
        void StartBattle( List<IFighter> fighters );
    }
}