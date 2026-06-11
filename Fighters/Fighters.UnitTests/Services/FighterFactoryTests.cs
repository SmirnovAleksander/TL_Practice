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
        // Arrange
        IRace race = CreateRace( "Человек", damage: 11, armor: 4, initiative: 2, health: 100 );
        IWeapon weapon = CreateWeapon( "Молот", damage: 6 );
        IArmor armor = CreateArmor( "Кольчужная броня", armor: 4 );
        IClass fighterClass = CreateClass( "Рыцарь", damage: 3, initiative: 1, health: 10 );

        SetupDataProvider( [ race ], [ weapon ], [ armor ], [ fighterClass ] );
        _inputHelperMock.Setup( i => i.ReadChoice( maxOption: 1 ) ).Returns( 1 );

        // Act
        IFighter fighter = _factory.CreateFighter( "Fighter" );

        // Assert
        Assert.Equal( "Fighter", fighter.Name );
        Assert.Equal( 20, fighter.CalculateDamage() );
        Assert.Equal( 8, fighter.CalculateArmor() );
        Assert.Equal( 3, fighter.Initiative );
        Assert.Equal( 110, fighter.GetMaxHealth() );
    }

    [Fact]
    public void CreateFighter_AllMenusDisplayed_ShowsAllFourChoices()
    {
        // Arrange
        IRace race = CreateRace( "Гном" );
        IWeapon weapon = CreateWeapon( "Копьё" );
        IArmor armor = CreateArmor( "Тканевая одежда" );
        IClass cls = CreateClass( "Ассасин" );

        SetupDataProvider( [ race ], [ weapon ], [ armor ], [ cls ] );
        _inputHelperMock.Setup( i => i.ReadChoice( maxOption: 1 ) ).Returns( 1 );

        // Act
        _factory.CreateFighter( "Fighter" );

        // Assert
        _consoleMock.Verify( c => c.WriteLine( "Выберите расу:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "1 - Гном" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите оружие:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "1 - Копьё" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите броню:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "1 - Тканевая одежда" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Выберите класс:" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "1 - Ассасин" ), Times.Once );
    }

    [Fact]
    public void CreateFighter_SelectSecondItem_ReturnsWeaponWithCorrectDamage()
    {
        // Arrange
        IWeapon firstWeapon = CreateWeapon( "Молот", damage: 6 );
        IWeapon secondWeapon = CreateWeapon( "Железный меч", damage: 4 );

        SetupDataProvider(
            [ EmptyRace() ],
            [ firstWeapon, secondWeapon ],
            [ EmptyArmor() ],
            [ EmptyClass() ] );
        _inputHelperMock.Setup( i => i.ReadChoice( maxOption: 1 ) ).Returns( 1 );
        _inputHelperMock.Setup( i => i.ReadChoice( maxOption: 2 ) ).Returns( 2 );

        // Act
        IFighter fighter = _factory.CreateFighter( "Fighter" );

        // Assert
        Assert.Equal( 4, fighter.CalculateDamage() );
    }

    private void SetupDataProvider(
        List<IRace> races,
        List<IWeapon> weapons,
        List<IArmor> armors,
        List<IClass> classes )
    {
        _dataProviderMock.Setup( d => d.Races ).Returns( races );
        _dataProviderMock.Setup( d => d.Weapons ).Returns( weapons );
        _dataProviderMock.Setup( d => d.Armors ).Returns( armors );
        _dataProviderMock.Setup( d => d.Classes ).Returns( classes );
    }

    private static IRace CreateRace(
        string name,
        int damage = 0,
        int armor = 0,
        int initiative = 0,
        int health = 0 )
    {
        Mock<IRace> mock = new();
        mock.Setup( r => r.Name ).Returns( name );
        mock.Setup( r => r.Damage ).Returns( damage );
        mock.Setup( r => r.Armor ).Returns( armor );
        mock.Setup( r => r.Initiative ).Returns( initiative );
        mock.Setup( r => r.Health ).Returns( health );
        return mock.Object;
    }

    private static IWeapon CreateWeapon( string name, int damage = 0 )
    {
        Mock<IWeapon> mock = new();
        mock.Setup( w => w.Name ).Returns( name );
        mock.Setup( w => w.Damage ).Returns( damage );
        return mock.Object;
    }

    private static IArmor CreateArmor( string name, int armor = 0 )
    {
        Mock<IArmor> mock = new();
        mock.Setup( a => a.Name ).Returns( name );
        mock.Setup( a => a.Armor ).Returns( armor );
        return mock.Object;
    }

    private static IClass CreateClass(
        string name,
        int damage = 0,
        int initiative = 0,
        int health = 0 )
    {
        Mock<IClass> mock = new();
        mock.Setup( c => c.Name ).Returns( name );
        mock.Setup( c => c.Damage ).Returns( damage );
        mock.Setup( c => c.Initiative ).Returns( initiative );
        mock.Setup( c => c.Health ).Returns( health );
        return mock.Object;
    }

    private static IRace EmptyRace()
    {
        return CreateRace( "" );
    }

    private static IArmor EmptyArmor()
    {
        return CreateArmor( "" );
    }

    private static IClass EmptyClass()
    {
        return CreateClass( "" );
    }
}
