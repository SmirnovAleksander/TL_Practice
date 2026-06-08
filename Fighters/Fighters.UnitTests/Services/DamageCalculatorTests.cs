using Fighters.Models.Fighters;
using Fighters.Services.Implementations;
using Fighters.Services.Interfaces;
using Moq;

namespace Fighters.UnitTests.Services;

public class DamageCalculatorTests
{
    private const int MinRandomModifier = -20;
    private const int MaxRandomModifier = 10;

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
    public void CalculateDamage_BaseDamageMinusArmor_ReturnsReducedDamage()
    {
        int baseDamage = 100;
        int armor = 20;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( baseDamage - armor, damage );
    }

    [Fact]
    public void CalculateDamage_NegativeModifier_ReduceDamage()
    {
        int baseDamage = 100;
        int randomModifier = -20;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        int expectedDamage = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) );
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_PositiveModifier_IncreasesDamage()
    {
        int baseDamage = 100;
        int randomModifier = 10;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        int expectedDamage = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) );
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalHit_DoublesDamageBeforeArmor()
    {
        int baseDamage = 100;
        int armor = 20;
        int randomModifier = 0;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        int expectedDamage = baseDamage * 2 - armor;
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_DamageLessThanArmor_ReturnsZero()
    {
        int baseDamage = 10;
        int armor = 50;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( 0, damage );
    }

    [Fact]
    public void CalculateDamage_ZeroArmor_ReturnsFullDamage()
    {
        int baseDamage = 50;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        Assert.Equal( baseDamage, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalWithNegativeModifier_AppliesBothMultipliers()
    {
        int baseDamage = 100;
        int armor = 10;
        int randomModifier = -10;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        int expectedDamage = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) ) * 2 - armor;
        Assert.Equal( expectedDamage, damage );
    }
}
