using Fighters.Data;
using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services.Interfaces;
using Fighters.Utils;

namespace Fighters.Services.Implementations;

public class FighterFactory : IFighterFactory
{
    private readonly IDataProvider _dataProvider;
    private readonly IInputHelper _inputHelper;

    public FighterFactory( IDataProvider dataProvider, IInputHelper inputHelper )
    {
        _dataProvider = dataProvider;
        _inputHelper = inputHelper;
    }

    public IFighter CreateFighter( string name )
    {
        IRace race = _inputHelper.SelectItem( _dataProvider.Races, r => r.Name, "Выберите расу:" );
        IWeapon weapon = _inputHelper.SelectItem( _dataProvider.Weapons, w => w.Name, "Выберите оружие:" );
        IArmor armor = _inputHelper.SelectItem( _dataProvider.Armors, a => a.Name, "Выберите броню:" );
        IClass fighterClass = _inputHelper.SelectItem( _dataProvider.Classes, c => c.Name, "Выберите класс:" );

        return new Fighter(
            name,
            race,
            armor,
            weapon,
            fighterClass );
    }
}