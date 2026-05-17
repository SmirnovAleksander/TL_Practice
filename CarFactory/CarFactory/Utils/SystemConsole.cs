namespace CarFactory.Utils;

public class SystemConsole : IConsole
{
    public void WriteLine() => Console.WriteLine();
    public void WriteLine( string message ) => Console.WriteLine( message );
    public string ReadLine() => Console.ReadLine() ?? "";
}
