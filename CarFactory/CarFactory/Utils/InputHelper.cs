namespace CarFactory.Utils;

public class InputHelper : IInputHelper
{
    private readonly IConsole _console;

    public InputHelper( IConsole console )
    {
        _console = console;
    }

    public int ReadChoice( int maxOption )
    {
        bool isValid = false;
        int choice = 0;

        while ( !isValid )
        {
            string input = _console.ReadLine();
            isValid = int.TryParse( input, out choice ) && choice >= 1 && choice <= maxOption;

            if ( !isValid )
            {
                _console.WriteLine( $"Введите число от 1 до {maxOption}" );
            }
        }

        return choice;
    }
}