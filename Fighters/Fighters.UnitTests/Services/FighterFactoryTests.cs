using Fighters.Data;
using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services.Implementations;
using Fighters.Utils;
using Moq;

namespace Fighters.UnitTests.Services;

public class FighterFactoryTests
{
    private readonly Mock<IDataProvider> _dataProviderMock;
    private readonly Mock<IInputHelper> _inputHelperMock;
    private readonly Mock<IConsole> _consoleMock;
    private readonly FighterFactory _factory;

    public FighterFactoryTests()
    {
        _dataProviderMock = new Mock<IDataProvider>();
        _inputHelperMock = new Mock<IInputHelper>();
        _consoleMock = new Mock<IConsole>();
        _factory = new FighterFactory( _dataProviderMock.Object, _inputHelperMock.Object, _consoleMock.Object );
    }

    [Fact]
    public void CreateFighter_ValidChoices_ReturnsFighterWithCorrectStats()
    {
        Mock<IRace> raceMock = new();
        raceMock.Setup( r => r.Name ).Returns( "Человек" );
        raceMock.Setup( r => r.Damage ).Returns( 11 );
        raceMock.Setup( r => r.Armor ).Returns( 4 );
        raceMock.Setup( r => r.Initiative ).Returns( 2 );
        raceMock.Setup( r => r.Health ).Returns( 100 );

        Mock<IWeapon> weaponMock = new();
        weaponMock.Setup( w => w.Name ).Returns( "Молот" );
        weaponMock.Setup( w => w.Damage ).Returns( 6 );

        Mock<IArmor> armorMock = new();
        armorMock.Setup( a => a.Name ).Returns( "Кольчужная броня" );
        armorMock.Setup( a => a.Armor ).Returns( 4 );

        Mock<IClass> classMock = new();
        classMock.Setup( c => c.Name ).Returns( "Рыцарь" );
        classMock.Setup( c => c.Damage ).Returns( 3 );
        classMock.Setup( c => c.Initiative ).Returns( 1 );
        classMock.Setup( c => c.Health ).Returns( 10 );

        _dataProviderMock.Setup( d => d.Races ).Returns( [ raceMock.Object ] );
        _dataProviderMock.Setup( d => d.Weapons ).Returns( [ weaponMock.Object ] );
        _dataProviderMock.Setup( d => d.Armors ).Returns( [ armorMock.Object ] );
        _dataProviderMock.Setup( d => d.Classes ).Returns( [ classMock.Object ] );
        _inputHelperMock.Setup( i => i.ReadChoice( 1 ) ).Returns( 1 );

        IFighter fighter = _factory.CreateFighter( "Fighter" );

        Assert.Equal( "Fighter", fighter.Name );
        Assert.Equal( 20, fighter.CalculateDamage() );
        Assert.Equal( 8, fighter.CalculateArmor() );
        Assert.Equal( 3, fighter.Initiative );
        Assert.Equal( 110, fighter.GetMaxHealth() );
    }

    [Fact]
    public void CreateFighter_ShowMenu()
    {
        Mock<IRace> race = new();
        race.Setup( r => r.Name ).Returns( "Гном" );
        Mock<IWeapon> weapon = new();
        weapon.Setup( w => w.Name ).Returns( "Копьё" );
        Mock<IArmor> armor = new();
        armor.Setup( a => a.Name ).Returns( "Тканевая одежда" );
        Mock<IClass> cls = new();
        cls.Setup( c => c.Name ).Returns( "Ассасин" );

        _dataProviderMock.Setup( d => d.Races ).Returns( [ race.Object ] );
        _dataProviderMock.Setup( d => d.Weapons ).Returns( [ weapon.Object ] );
        _dataProviderMock.Setup( d => d.Armors ).Returns( [ armor.Object ] );
        _dataProviderMock.Setup( d => d.Classes ).Returns( [ cls.Object ] );
        _inputHelperMock.Setup( i => i.ReadChoice( 1 ) ).Returns( 1 );

        _factory.CreateFighter( "Fighter" );

        _consoleMock.Verify( c => c.WriteLine( "Выберите расу:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите оружие:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите броню:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите класс:" ), Times.Once );
    }

    [Fact]
    public void CreateFighter_SelectSecondItem_ReturnWeapon()
    {
        Mock<IWeapon> firstWeapon = new();
        firstWeapon.Setup( w => w.Name ).Returns( "Молот" );
        firstWeapon.Setup( w => w.Damage ).Returns( 6 );

        Mock<IWeapon> secondWeapon = new();
        secondWeapon.Setup( w => w.Name ).Returns( "Железный меч" );
        secondWeapon.Setup( w => w.Damage ).Returns( 4 );

        _dataProviderMock.Setup( d => d.Races ).Returns( [ EmptyRace() ] );
        _dataProviderMock.Setup( d => d.Weapons ).Returns( [ firstWeapon.Object, secondWeapon.Object ] );
        _dataProviderMock.Setup( d => d.Armors ).Returns( [ EmptyArmor() ] );
        _dataProviderMock.Setup( d => d.Classes ).Returns( [ EmptyClass() ] );
        _inputHelperMock.Setup( i => i.ReadChoice( 1 ) ).Returns( 1 );
        _inputHelperMock.Setup( i => i.ReadChoice( 2 ) ).Returns( 2 );

        IFighter fighter = _factory.CreateFighter( "Fighter" );

        Assert.Equal( 4, fighter.CalculateDamage() );
    }

    private static IRace EmptyRace()
    {
        Mock<IRace> mock = new();
        mock.Setup( r => r.Damage ).Returns( 0 );
        mock.Setup( r => r.Initiative ).Returns( 0 );
        mock.Setup( r => r.Health ).Returns( 0 );
        mock.Setup( r => r.Armor ).Returns( 0 );
        mock.Setup( r => r.Name ).Returns( "" );
        return mock.Object;
    }

    private static IArmor EmptyArmor()
    {
        Mock<IArmor> mock = new();
        mock.Setup( a => a.Armor ).Returns( 0 );
        mock.Setup( a => a.Name ).Returns( "" );
        return mock.Object;
    }

    private static IClass EmptyClass()
    {
        Mock<IClass> mock = new();
        mock.Setup( c => c.Damage ).Returns( 0 );
        mock.Setup( c => c.Initiative ).Returns( 0 );
        mock.Setup( c => c.Health ).Returns( 0 );
        mock.Setup( c => c.Name ).Returns( "" );
        return mock.Object;
    }
}
