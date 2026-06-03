namespace Fighters.Utils;

public class SystemConsole : IConsole
{
    public void WriteLine( object? message ) => Console.WriteLine( message );
    public string ReadLine() => Console.ReadLine() ?? "";
}
