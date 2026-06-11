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
        // Arrange
        int baseDamage = 100;
        int armor = 20;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        Assert.Equal( baseDamage - armor, damage );
    }

    [Fact]
    public void CalculateDamage_NegativeModifier_ReduceDamage()
    {
        // Arrange
        int baseDamage = 100;
        int randomModifier = -20;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        int expectedDamage = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) ); // −20% -> 80
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_PositiveModifier_IncreasesDamage()
    {
        // Arrange
        int baseDamage = 100;
        int randomModifier = 10;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        int expectedDamage = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) ); // +10% -> 110
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalHit_DoublesDamageBeforeArmor()
    {
        // Arrange
        int baseDamage = 100;
        int armor = 20;
        int randomModifier = 0;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        int expectedDamage = baseDamage * 2 - armor; // 100 ×2 крит −20 броня -> 180
        Assert.Equal( expectedDamage, damage );
    }

    [Fact]
    public void CalculateDamage_DamageLessThanArmor_ReturnsZero()
    {
        // Arrange
        int baseDamage = 10;
        int armor = 50;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        Assert.Equal( 0, damage );
    }

    [Fact]
    public void CalculateDamage_ZeroArmor_ReturnsFullDamage()
    {
        // Arrange
        int baseDamage = 50;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( 0 );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( 0 );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        Assert.Equal( baseDamage, damage );
    }

    [Fact]
    public void CalculateDamage_CriticalWithNegativeModifier_AppliesBothMultipliers()
    {
        // Arrange
        int baseDamage = 100;
        int armor = 10;
        int randomModifier = -10;
        _attackerMock.Setup( a => a.CalculateDamage() ).Returns( baseDamage );
        _defenderMock.Setup( d => d.CalculateArmor() ).Returns( armor );
        _randomProviderMock.Setup( r => r.Next( MinRandomModifier, MaxRandomModifier + 1 ) ).Returns( randomModifier );
        _randomProviderMock.Setup( r => r.NextDouble() ).Returns( 0.0 );

        // Act
        int damage = _calculator.CalculateDamage( _attackerMock.Object, _defenderMock.Object );

        // Assert
        // randomModifier -10%: 100 * 0.9 = 90
        int afterRandomModifier = ( int )( baseDamage * ( 1.0 + randomModifier / 100.0 ) );
        // крит x2: 90 * 2 = 180
        int afterCrit = afterRandomModifier * 2;
        // вычет брони: 180 - 10 = 170
        int expectedDamage = afterCrit - armor;
        Assert.Equal( expectedDamage, damage );
    }
}
