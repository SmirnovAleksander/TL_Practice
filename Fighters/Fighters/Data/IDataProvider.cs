using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Data;

public interface IDataProvider
{
    List<IRace> Races { get; }
    List<IWeapon> Weapons { get; }
    List<IArmor> Armors { get; }
    List<IClass> Classes { get; }
}
