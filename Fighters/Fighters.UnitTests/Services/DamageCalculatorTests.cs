using Fighters.Models.Fighters;
using Fighters.Services.Implementations;
using Fighters.Services.Interfaces;
using Moq;

namespace Fighters.UnitTests.Services;

public class DamageCalculatorTests
{
    private readonly Mock<IRandomProvider> _randomProviderMock;
    private readonly Mock<IFighter> _attackerMock;
    private readonly Mock<IFighter> _defenderMock;
    private readonly DamageCalculator _calculator;

    public DamageCalculatorTests()
    {
        _randomProviderMock = new Mock<IRandomProvider>();
        _attackerMock = new Mock<IFighter>();
        _defenderMock = new Mock<IFighter>();
        _calculator = new DamageCalculator( _randomProviderMock.Object );
    }

    [Fact]
    public void CalculateDamage_ReturnBaseDamageMinusArmor()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 100 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 20 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 80, damage );
    }

    [Fact]
    public void CalculateDamage_NegativeRandomDamage()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 100 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( -20 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 80, damage );
    }

    [Fact]
    public void CalculateDamage_PositiveRandomDamage()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 100 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( 10 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 110, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalHitDamage()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 100 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 20 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 180, damage );
    }

    [Fact]
    public void CalculateDamage_DamageLessThanArmor_ReturnsZero()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 10 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 50 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 0, damage );
    }

    [Fact]
    public void CalculateDamage_ZeroArmor_ReturnsFullDamage()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 50 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 50, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalWithNegativeModifier()
    {
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( 100 );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 10 );
        _randomProviderMock.Setup( r => r.Next( -20, 11 ) ).Returns( -10 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 170, damage );
    }
}
