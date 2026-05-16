using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Class;
using Fighters.Models.Weapons;
using Fighters.Models.Armors;
using Fighters.Services.Interfaces;
using Fighters.Utils;

namespace Fighters.Services.Implementations;

public class FighterFactory : IFighterFactory
{
    private readonly List<IRace> _races;
    private readonly List<IWeapon> _weapons;
    private readonly List<IArmor> _armors;
    private readonly List<IClass> _classes;

    public FighterFactory( 
        List<IRace> races, 
        List<IWeapon> weapons, 
        List<IArmor> armors, 
        List<IClass> classes )
    {
        _races = races;
        _weapons = weapons;
        _armors = armors;
        _classes = classes;
    }

    public IFighter CreateFighter( string name )
    {
        IRace race = InputHelper.SelectItem( _races, r => r.Name, "Выберите расу:" );
        IWeapon weapon = InputHelper.SelectItem( _weapons, w => w.Name, "Выберите оружие:" );
        IArmor armor = InputHelper.SelectItem( _armors, a => a.Name, "Выберите броню:" );
        IClass fighterClass = InputHelper.SelectItem( _classes, c => c.Name, "Выберите класс:" );

        return new Fighter( name, race, armor, weapon, fighterClass );
    }
}