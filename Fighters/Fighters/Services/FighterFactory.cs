using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Services
{
    public class FighterFactory
    {
        private List<IRace> _races = new()
        {
            new Human(),
            new Dwarf(),
            new Golem(),
            new Ghost(),
        };

        private List<IWeapon> _weapons = new()
        {
            new NoWeapon(),
            new SilverSword(),
            new Spear(),
            new Hammer()
        };

        private List<IArmor> _armors = new()
        {
            new NoArmor(),
            new ClothRobe(),
            new LeatherArmor(),
            new ChainMailArmor(),
            new PlateArmor(),
        };

        private List<IClass> _classes = new()
        {
            new Wizard(),
            new Assassin(),
            new Knight()
        };

        public IFighter CreateFighter( string name )
        {
            Console.WriteLine( "Выберите расу:" );
            DisplayOptions( _races.Select( r => r.Name ).ToList() );
            int raceIndex = ReadChoice( _races.Count );

            Console.WriteLine( "Выберите оружие:" );
            DisplayOptions( _weapons.Select( w => w.Name ).ToList() );
            int weaponIndex = ReadChoice( _weapons.Count );

            Console.WriteLine( "Выберите броню:" );
            DisplayOptions( _armors.Select( a => a.Name ).ToList() );
            int armorIndex = ReadChoice( _armors.Count );

            Console.WriteLine( "Выберите класс:" );
            DisplayOptions( _classes.Select( c => c.Name ).ToList() );
            int classIndex = ReadChoice( _classes.Count );

            return new Fighter(
                name,
                _races[ raceIndex ],
                _armors[ armorIndex ],
                _weapons[ weaponIndex ],
                _classes[ classIndex ]
            );
        }

        private void DisplayOptions( List<string> options )
        {
            for ( int i = 0; i < options.Count; i++ )
            {
                Console.WriteLine( $"{i} - {options[ i ]}" );
            }
        }

        private int ReadChoice( int maxOption )
        {
            while ( true )
            {
                string input = Console.ReadLine() ?? "";
                if ( int.TryParse( input, out int choice ) && choice >= 0 && choice < maxOption )
                {
                    return choice;
                }
                Console.WriteLine( $"Введите число от 0 до {maxOption - 1}" );
            }
        }
    }
}