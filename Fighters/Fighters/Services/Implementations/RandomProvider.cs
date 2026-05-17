using Fighters.Services.Interfaces;

namespace Fighters.Services.Implementations;

public class RandomProvider : IRandomProvider
{
    public int Next( int minValue, int maxValue ) => Random.Shared.Next( minValue, maxValue );
    public double NextDouble() => Random.Shared.NextDouble();
}
