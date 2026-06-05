using Fighters.Services.Implementations;

namespace Fighters.UnitTests.Services;

public class RandomProviderTests
{
    private readonly RandomProvider _randomProvider = new();

    [Fact]
    public void Next_ReturnsValueWithinRange()
    {
        int result = _randomProvider.Next( 5, 10 );

        Assert.InRange( result, 5, 9 );
    }

    [Fact]
    public void Next_MinEqualToMax_ReturnsMin()
    {
        int result = _randomProvider.Next( 7, 8 );

        Assert.Equal( 7, result );
    }

    [Fact]
    public void NextDouble_ReturnsValueBetweenZeroAndOne()
    {
        double result = _randomProvider.NextDouble();

        Assert.InRange( result, 0.0, 1.0 );
    }
}
