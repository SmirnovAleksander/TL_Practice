namespace Fighters.Utils;

public interface IConsole
{
    void WriteLine( object? message );
    string ReadLine();
}
