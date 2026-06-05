namespace CarFactory.Utils;

public interface IConsole
{
    void WriteLine( object? message );
    string ReadLine();
}
