using Fighters.Models.Fighters;
using Fighters.Services;

namespace Fighters;

internal class Program
{
    static void Main( string[] args )
    {
        FighterFactory factory = new FighterFactory();
        BattleManager battleManager = new BattleManager();

        List<IFighter> fighters = new List<IFighter>();

        while ( true )
        {
            ShowMenu();
            int choice = ReadChoice( 4 );

            switch ( choice )
            {
                case 1:
                    AddFighter( factory, fighters );
                    break;

                case 2:
                    ShowFighters( fighters );
                    break;

                case 3:
                    Play( battleManager, fighters );
                    break;

                case 4:
                    return;
            }
        }
    }

    public static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine( "Fighters Game - Меню" );
        Console.WriteLine( "1 - Добавить бойца" );
        Console.WriteLine( "2 - Показать бойцов" );
        Console.WriteLine( "3 - Играть" );
        Console.WriteLine( "4 - Выйти" );
    }

    public static void AddFighter( FighterFactory factory, List<IFighter> fighters )
    {
        Console.WriteLine();
        Console.WriteLine( "Введите имя бойца:" );
        string name = Console.ReadLine() ?? "";

        if ( string.IsNullOrEmpty( name ) )
        {
            Console.WriteLine( "Имя не может быть пустым!" );
            return;
        }

        IFighter fighter = factory.CreateFighter( name );
        fighters.Add( fighter );
        Console.WriteLine( "Боец добавлен!" );
    }

    public static void ShowFighters( List<IFighter> fighters )
    {
        Console.WriteLine();

        if ( fighters.Count == 0 )
        {
            Console.WriteLine( "Нет бойцов!" );
            return;
        }

        Console.WriteLine( "Список бойцов:" );
        for ( int i = 0; i < fighters.Count; i++ )
        {
            IFighter f = fighters[ i ];
            Console.WriteLine( ( i + 1 ) + ". " + f.Name );
            Console.WriteLine( "   Класс: " + f.GetClassName() + ", Раса: " + f.GetRaceName() + ", Броня на герое: " + f.GetArmorName() + ", Оружие: " + f.GetWeaponName() );
            Console.WriteLine( "   HP: " + f.GetMaxHealth() + ", Урон: " + f.CalculateDamage() + ", Броня: " + f.CalculateArmor() + ", Инициатива: " + f.Initiative );
        }
    }

    public static void Play( BattleManager battleManager, List<IFighter> fighters )
    {
        Console.WriteLine();

        if ( fighters.Count < 2 )
        {
            Console.WriteLine( "Для битвы нужно минимум 2 бойца!" );
            return;
        }

        battleManager.StartBattle( fighters );
        fighters.Clear();
    }

    public static int ReadChoice( int maxOption )
    {
        while ( true )
        {
            Console.WriteLine();
            Console.Write( "Выберите пункт: " );
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out int choice ) && choice >= 1 && choice <= maxOption )
            {
                return choice;
            }
            Console.WriteLine( "Введите число от 1 до " + maxOption );
        }
    }
}