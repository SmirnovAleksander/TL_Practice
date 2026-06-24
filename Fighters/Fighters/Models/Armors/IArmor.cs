using Fighters.Models.Interfaces;

namespace Fighters.Models.Armors;

public interface IArmor : INamed
{
    public int Armor { get; }
}