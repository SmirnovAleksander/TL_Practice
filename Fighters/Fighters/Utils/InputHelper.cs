namespace Fighters.Utils;

public class InputHelper
{
    public static int ReadChoice( int maxOption )
    {
        while ( true )
        {
            string input = Console.ReadLine() ?? "";
            if ( int.TryParse( input, out int choice ) && choice >= 0 && choice < maxOption )
            {
                return choice;
            }
            Console.WriteLine( "Введите число от 0 до " + ( maxOption - 1 ) );
        }
    }

    public static void DisplayOptions( List<string> options )
    {
        for ( int i = 0; i < options.Count; i++ )
        {
            Console.WriteLine( i + " - " + options[ i ] );
        }
    }
}