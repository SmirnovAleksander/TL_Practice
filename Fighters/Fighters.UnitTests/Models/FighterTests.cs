using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.UnitTests.Models;

public class FighterTests
{
    private readonly Mock<IRace> _raceMock;
    private readonly Mock<IClass> _classMock;
    private readonly Mock<IWeapon> _weaponMock;
    private readonly Mock<IArmor> _armorMock;

    public FighterTests()
    {
        _raceMock = new Mock<IRace>();
        _classMock = new Mock<IClass>();
        _weaponMock = new Mock<IWeapon>();
        _armorMock = new Mock<IArmor>();
    }

    [Fact]
    public void Constructor_ValidName_SetsName()
    {
        // Act
        IFighter fighter = CreateFighter( "Fighter" );

        // Assert
        Assert.Equal( "Fighter", fighter.Name );
    }

    [Fact]
    public void GetMaxHealth_RaceAndClassHealthSum_ReturnsTotalMaxHealth()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 80 );
        _classMock.Setup( c => c.Health ).Returns( 20 );

        IFighter fighter = CreateFighter( "MaxHealthTest" );

        // Act
        int maxHealth = fighter.GetMaxHealth();

        // Assert
        Assert.Equal( 100, maxHealth );
    }

    [Fact]
    public void CalculateDamage_AllComponentsSum_ReturnsTotalDamage()
    {
        // Arrange
        _weaponMock.Setup( w => w.Damage ).Returns( 5 );
        _classMock.Setup( c => c.Damage ).Returns( 3 );
        _raceMock.Setup( r => r.Damage ).Returns( 10 );

        IFighter fighter = CreateFighter( "DamageTest" );

        // Act
        int damage = fighter.CalculateDamage();

        // Assert
        Assert.Equal( 18, damage );
    }

    [Fact]
    public void CalculateArmor_ArmorAndRaceArmorSum_ReturnsTotalArmor()
    {
        // Arrange
        _armorMock.Setup( a => a.Armor ).Returns( 8 );
        _raceMock.Setup( r => r.Armor ).Returns( 4 );

        IFighter fighter = CreateFighter( "ArmorTest" );

        // Act
        int armor = fighter.CalculateArmor();

        // Assert
        Assert.Equal( 12, armor );
    }

    [Fact]
    public void Initiative_RaceAndClassInitiativeSum_ReturnsTotalInitiative()
    {
        // Arrange
        _raceMock.Setup( r => r.Initiative ).Returns( 3 );
        _classMock.Setup( c => c.Initiative ).Returns( 2 );

        IFighter fighter = CreateFighter( "InitiativeTest" );

        // Act
        int initiative = fighter.Initiative;

        // Assert
        Assert.Equal( 5, initiative );
    }

    [Fact]
    public void TakeDamage_DamageLessThanHealth_ReduceCurrentHealth()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "DamageTest" );
        int initialHealth = fighter.GetCurrentHealth();

        // Act
        fighter.TakeDamage( 30 );

        // Assert
        Assert.Equal( initialHealth - 30, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreHealth_SetHealthToZero()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 50 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "ZeroTest" );

        // Act
        fighter.TakeDamage( 100 );

        // Assert
        Assert.Equal( 0, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void IsAlive_HealthGreaterThanZero_ReturnsTrue()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "TrueTest" );

        // Act
        bool isAlive = fighter.IsAlive();

        // Assert
        Assert.True( isAlive );
    }

    [Fact]
    public void IsAlive_HealthZero_ReturnsFalse()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 50 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "FalseTest" );
        fighter.TakeDamage( 100 );

        // Act
        bool isAlive = fighter.IsAlive();

        // Assert
        Assert.False( isAlive );
    }

    [Fact]
    public void GetCurrentHealth_AfterDamage_ReturnsMaxHealthMinusDamage()
    {
        // Arrange
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 10 );

        IFighter fighter = CreateFighter( "HealthTest" );

        // Act
        int currentHealthAfterCreation = fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( fighter.GetMaxHealth(), currentHealthAfterCreation );

        // Arrange
        fighter.TakeDamage( 30 );

        // Act
        int currentHealthAfterDamage = fighter.GetCurrentHealth();

        // Assert
        Assert.Equal( fighter.GetMaxHealth() - 30, currentHealthAfterDamage );
    }

    private IFighter CreateFighter( string name )
    {
        return new Fighter(
            name,
            _raceMock.Object,
            _armorMock.Object,
            _weaponMock.Object,
            _classMock.Object );
    }
}
