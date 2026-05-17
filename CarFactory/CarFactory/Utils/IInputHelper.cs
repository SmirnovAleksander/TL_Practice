namespace CarFactory.Utils;

public interface IInputHelper
{
    int ReadChoice( int maxOption );
    T SelectItem<T>( List<T> items, Func<T, string> nameSelector, string textMessage );
}
