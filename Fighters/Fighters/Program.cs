using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services.Interfaces;
using Fighters.Services.Implementations;
using Fighters.Utils;

namespace Fighters
{
    internal class Program
    {
        static void Main()
        {
            List<IRace> races = new List<IRace>
            {
                new Human(),
                new Dwarf(),
                new Golem(),
                new Ghost()
            };

            List<IWeapon> weapons = new List<IWeapon>
            {
                new NoWeapon(),
                new SilverSword(),
                new Spear(),
                new Hammer()
            };

            List<IArmor> armors = new List<IArmor>
            {
                new NoArmor(),
                new ClothRobe(),
                new LeatherArmor(),
                new ChainMailArmor(),
                new PlateArmor()
            };

            List<IClass> classes = new List<IClass>
            {
                new Knight(),
                new Wizard(),
                new Assassin()
            };

            IFighterFactory factory = new FighterFactory( races, weapons, armors, classes );
            IDamageCalculator damageCalculator = new DamageCalculator();
            IBattleManager battleManager = new BattleManager( damageCalculator );
            IGameManager gameManager = new GameManager( factory, battleManager );

            List<IFighter> fighters = new List<IFighter>();

            const int maxOptionIndex = 4;

            while ( true )
            {
                gameManager.ShowMenu();
                int choice = InputHelper.ReadChoice( maxOptionIndex );

                switch ( choice )
                {
                    case 1:
                        gameManager.AddFighter( fighters );
                        break;

                    case 2:
                        gameManager.ShowFighters( fighters );
                        break;

                    case 3:
                        gameManager.Play( fighters );
                        break;

                    case 4:
                        return;
                }
            }
        }
    }
}