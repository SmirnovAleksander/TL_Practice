using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services.Implementations;
using Fighters.Services.Interfaces;
using Fighters.Utils;
using Moq;

namespace Fighters.UnitTests.Services;

public class BattleManagerTests
{
    private readonly Mock<IDamageCalculator> _damageCalculatorMock;
    private readonly Mock<IConsole> _consoleMock;
    private readonly BattleManager _battleManager;

    public BattleManagerTests()
    {
        _damageCalculatorMock = new Mock<IDamageCalculator>();
        _consoleMock = new Mock<IConsole>();
        _battleManager = new BattleManager( _damageCalculatorMock.Object, _consoleMock.Object );
    }

    [Fact]
    public void StartBattle_LessThanTwoFighters_WritesError()
    {
        // Arrange
        IFighter fighter = CreateFighter( name: "Fighter", initiative: 10, health: 100 );

        // Act
        _battleManager.StartBattle( [ fighter ] );

        // Assert
        _damageCalculatorMock.Verify( d => d.CalculateDamage( It.IsAny<IFighter>(), It.IsAny<IFighter>() ), Times.Never );
        Assert.True( fighter.IsAlive() );
    }

    [Fact]
    public void StartBattle_TwoFighters_DeclaresWinner()
    {
        // Arrange
        IFighter fighter1 = CreateFighter( name: "Fighter1", initiative: 10, health: 200 );
        IFighter fighter2 = CreateFighter( name: "Fighter2", initiative: 5, health: 1 );

        _damageCalculatorMock
            .Setup( d => d.CalculateDamage( It.IsAny<IFighter>(), It.IsAny<IFighter>() ) )
            .Returns( 100 );

        // Act
        _battleManager.StartBattle( [ fighter1, fighter2 ] );

        // Assert
        Assert.True( fighter1.IsAlive() );
        Assert.False( fighter2.IsAlive() );
    }

    [Fact]
    public void StartBattle_ThreeFighters_RemovesDeadAndDeclaresWinner()
    {
        // Arrange
        IFighter fighter1 = CreateFighter( name: "Fighter1", initiative: 10, health: 1000 );
        IFighter fighter2 = CreateFighter( name: "Fighter2", initiative: 5, health: 1 );
        IFighter fighter3 = CreateFighter( name: "Fighter3", initiative: 1, health: 1 );

        _damageCalculatorMock
            .Setup( d => d.CalculateDamage( It.IsAny<IFighter>(), It.IsAny<IFighter>() ) )
            .Returns( 100 );

        // Act
        _battleManager.StartBattle( [ fighter1, fighter2, fighter3 ] );

        // Assert
        Assert.True( fighter1.IsAlive() );
        Assert.False( fighter2.IsAlive() );
        Assert.False( fighter3.IsAlive() );
    }

    private static IFighter CreateFighter( string name, int initiative, int health )
    {
        Mock<IRace> raceMock = new();
        raceMock.Setup( r => r.Name ).Returns( name );
        raceMock.Setup( r => r.Initiative ).Returns( initiative );
        raceMock.Setup( r => r.Health ).Returns( health );

        Mock<IClass> classMock = new();
        classMock.Setup( c => c.Health ).Returns( 0 );

        return new Fighter(
            name,
            raceMock.Object,
            new Mock<IArmor>().Object,
            new Mock<IWeapon>().Object,
            classMock.Object );
    }
}
