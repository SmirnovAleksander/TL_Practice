namespace CarFactory.Utils;

public class InputHelper
{
    public static int ReadChoice( int maxOption )
    {
        while ( true )
        {
            string input = Console.ReadLine() ?? "";
            if ( int.TryParse( input, out int choice ) && choice >= 1 && choice <= maxOption )
            {
                return choice;
            }
            Console.WriteLine( $"Введите число от 1 до {maxOption}" );
        }
    }

    public static T SelectItem<T>( List<T> items, Func<T, string> nameSelector, string textMessage )
    {
        Console.WriteLine( textMessage );
        for ( int i = 0; i < items.Count; i++ )
        {
            Console.WriteLine( $"{i + 1} - {nameSelector( items[ i ] )}" );
        }
        int choice = ReadChoice( items.Count );
        return items[ choice - 1 ];
    }
}