using Fighters.Data;
using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Interfaces;
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
        IRace race = SelectItem( _dataProvider.Races, "Выберите расу:" );
        IWeapon weapon = SelectItem( _dataProvider.Weapons, "Выберите оружие:" );
        IArmor armor = SelectItem( _dataProvider.Armors, "Выберите броню:" );
        IClass fighterClass = SelectItem( _dataProvider.Classes, "Выберите класс:" );

        return new Fighter(
            name,
            race,
            armor,
            weapon,
            fighterClass );
    }

    private T SelectItem<T>( List<T> items, string textMessage ) where T : INamed
    {
        Console.WriteLine( textMessage );
        for ( int i = 0; i < items.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {items[ i ].Name}" );
        }
        int choice = _inputHelper.ReadChoice( items.Count );
        return items[ choice - 1 ];
    }
}