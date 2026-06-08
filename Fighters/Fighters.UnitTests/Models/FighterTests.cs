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
        IFighter fighter = CreateFighter( "Fighter" );

        Assert.Equal( "Fighter", fighter.Name );
    }

    [Fact]
    public void GetMaxHealth_RaceAndClassHealthSum_ReturnsTotalMaxHealth()
    {
        _raceMock.Setup( r => r.Health ).Returns( 80 );
        _classMock.Setup( c => c.Health ).Returns( 20 );

        IFighter fighter = CreateFighter( "MaxHealthTest" );

        Assert.Equal( 100, fighter.GetMaxHealth() );
    }

    [Fact]
    public void CalculateDamage_AllComponentsSum_ReturnsTotalDamage()
    {
        _weaponMock.Setup( w => w.Damage ).Returns( 5 );
        _classMock.Setup( c => c.Damage ).Returns( 3 );
        _raceMock.Setup( r => r.Damage ).Returns( 10 );

        IFighter fighter = CreateFighter( "DamageTest" );

        Assert.Equal( 18, fighter.CalculateDamage() );
    }

    [Fact]
    public void CalculateArmor_ArmorAndRaceArmorSum_ReturnsTotalArmor()
    {
        _armorMock.Setup( a => a.Armor ).Returns( 8 );
        _raceMock.Setup( r => r.Armor ).Returns( 4 );

        IFighter fighter = CreateFighter( "ArmorTest" );

        Assert.Equal( 12, fighter.CalculateArmor() );
    }

    [Fact]
    public void Initiative_RaceAndClassInitiativeSum_ReturnsTotalInitiative()
    {
        _raceMock.Setup( r => r.Initiative ).Returns( 3 );
        _classMock.Setup( c => c.Initiative ).Returns( 2 );

        IFighter fighter = CreateFighter( "InitiativeTest" );

        Assert.Equal( 5, fighter.Initiative );
    }

    [Fact]
    public void TakeDamage_DamageLessThanHealth_ReduceCurrentHealth()
    {
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "DamageTest" );
        int initialHealth = fighter.GetCurrentHealth();

        fighter.TakeDamage( 30 );

        Assert.Equal( initialHealth - 30, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreHealth_SetHealthToZero()
    {
        _raceMock.Setup( r => r.Health ).Returns( 50 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "ZeroTest" );

        fighter.TakeDamage( 100 );

        Assert.Equal( 0, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void IsAlive_HealthGreaterThanZero_ReturnsTrue()
    {
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "TrueTest" );

        Assert.True( fighter.IsAlive() );
    }

    [Fact]
    public void IsAlive_HealthZero_ReturnsFalse()
    {
        _raceMock.Setup( r => r.Health ).Returns( 50 );
        _classMock.Setup( c => c.Health ).Returns( 0 );

        IFighter fighter = CreateFighter( "FalseTest" );
        fighter.TakeDamage( 100 );

        Assert.False( fighter.IsAlive() );
    }

    [Fact]
    public void GetCurrentHealth_AfterDamage_ReturnsMaxHealthMinusDamage()
    {
        _raceMock.Setup( r => r.Health ).Returns( 100 );
        _classMock.Setup( c => c.Health ).Returns( 10 );

        IFighter fighter = CreateFighter( "HealthTest" );

        Assert.Equal( fighter.GetMaxHealth(), fighter.GetCurrentHealth() );

        fighter.TakeDamage( 30 );

        Assert.Equal( fighter.GetMaxHealth() - 30, fighter.GetCurrentHealth() );
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
