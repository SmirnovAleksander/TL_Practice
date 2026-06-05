using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Data;

public class GameData : IDataProvider
{
    public List<IRace> Races { get; } =
    [
        new Human(),
        new Dwarf(),
        new Golem(),
        new Ghost()
    ];

    public List<IWeapon> Weapons { get; } =
    [
        new NoWeapon(),
        new SilverSword(),
        new Spear(),
        new Hammer()
    ];

    public List<IArmor> Armors { get; } =
    [
        new NoArmor(),
        new ClothRobe(),
        new LeatherArmor(),
        new ChainMailArmor(),
        new PlateArmor()
    ];

    public List<IClass> Classes { get; } =
    [
        new Knight(),
        new Wizard(),
        new Assassin()
    ];
}
