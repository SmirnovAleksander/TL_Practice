using Fighters.Models.Fighters;

namespace Fighters.Services.Interfaces;

public interface IGameManager
{
    void ShowMenu();
    void AddFighter( List<IFighter> fighters );
    void ShowFighters( List<IFighter> fighters );
    void Play( List<IFighter> fighters );
}