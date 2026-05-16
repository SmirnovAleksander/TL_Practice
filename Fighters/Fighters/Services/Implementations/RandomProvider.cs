using Fighters.Services.Interfaces;

namespace Fighters.Services.Implementations;

public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new();

    public int Next( int minValue, int maxValue ) => _random.Next( minValue, maxValue );
    public double NextDouble() => _random.NextDouble();
}
