using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utils;

namespace Fighters.Services
{
    public class FighterFactory : IFighterFactory
    {
        private readonly List<IRace> _races;
        private readonly List<IWeapon> _weapons;
        private readonly List<IArmor> _armors;
        private readonly List<IClass> _classes;

        public FighterFactory( List<IRace> races, List<IWeapon> weapons, List<IArmor> armors, List<IClass> classes )
        {
            _races = races;
            _weapons = weapons;
            _armors = armors;
            _classes = classes;
        }

        public IFighter CreateFighter( string name )
        {
            Console.WriteLine( "Выберите расу:" );
            InputHelper.DisplayOptions( _races.ConvertAll( r => r.Name ) );
            int raceIndex = InputHelper.ReadChoice( _races.Count );

            Console.WriteLine( "Выберите оружие:" );
            InputHelper.DisplayOptions( _weapons.ConvertAll( w => w.Name ) );
            int weaponIndex = InputHelper.ReadChoice( _weapons.Count );

            Console.WriteLine( "Выберите броню:" );
            InputHelper.DisplayOptions( _armors.ConvertAll( a => a.Name ) );
            int armorIndex = InputHelper.ReadChoice( _armors.Count );

            Console.WriteLine( "Выберите класс:" );
            InputHelper.DisplayOptions( _classes.ConvertAll( c => c.Name ) );
            int classIndex = InputHelper.ReadChoice( _classes.Count );

            return new Fighter(
                name,
                _races[ raceIndex ],
                _armors[ armorIndex ],
                _weapons[ weaponIndex ],
                _classes[ classIndex ]
            );
        }
    }
}