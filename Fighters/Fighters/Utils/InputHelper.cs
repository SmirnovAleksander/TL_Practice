namespace Fighters.Utils;

public class InputHelper : IInputHelper
{
    public int ReadChoice( int maxOption )
    {
        bool isValid = false;
        int choice = 0;

        while ( !isValid )
        {
            string input = Console.ReadLine() ?? "";
            isValid = int.TryParse( input, out choice ) && choice >= 1 && choice <= maxOption;

            if ( !isValid )
            {
                Console.WriteLine( $"Введите число от 1 до {maxOption}" );
            }
        }

        return choice;
    }
}