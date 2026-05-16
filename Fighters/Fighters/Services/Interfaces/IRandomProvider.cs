namespace Fighters.Services.Interfaces;

public interface IRandomProvider
{
    int Next( int minValue, int maxValue );
    double NextDouble();
}
