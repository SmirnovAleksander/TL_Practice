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
            if ( int.TryParse( input, out choice ) && choice >= 1 && choice <= maxOption )
            {
                isValid = true;
            }
            else
            {
                _console.WriteLine( $"Введите число от 1 до {maxOption}" );
            }
        }

        return choice;
    }

    public T SelectItem<T>( List<T> items, Func<T, string> nameSelector, string textMessage )
    {
        _console.WriteLine( textMessage );
        for ( int i = 0; i < items.Count; i++ )
        {
            _console.WriteLine( $"{i + 1} - {nameSelector( items[ i ] )}" );
        }
        int choice = ReadChoice( items.Count );
        return items[ choice - 1 ];
    }
}