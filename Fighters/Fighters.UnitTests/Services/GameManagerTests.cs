using Fighters.Models.Fighters;
using Fighters.Services.Implementations;
using Fighters.Services.Interfaces;
using Fighters.Utils;
using Moq;

namespace Fighters.UnitTests.Services;

public class GameManagerTests
{
    private readonly Mock<IFighterFactory> _factoryMock;
    private readonly Mock<IBattleManager> _battleManagerMock;
    private readonly Mock<IInputHelper> _inputHelperMock;
    private readonly Mock<IConsole> _consoleMock;
    private readonly GameManager _gameManager;

    public GameManagerTests()
    {
        _factoryMock = new Mock<IFighterFactory>();
        _battleManagerMock = new Mock<IBattleManager>();
        _inputHelperMock = new Mock<IInputHelper>();
        _consoleMock = new Mock<IConsole>();
        _gameManager = new GameManager(
            _factoryMock.Object,
            _battleManagerMock.Object,
            _inputHelperMock.Object,
            _consoleMock.Object );
    }

    [Fact]
    public void PlayGame_Choice_4_Exit()
    {
        _inputHelperMock.Setup( i => i.ReadChoice( 4 ) ).Returns( 4 );

        _gameManager.PlayGame();

        _consoleMock.Verify( c => c.WriteLine( "Fighters Game - Меню" ), Times.Once );
    }

    [Fact]
    public void PlayGame_Choice_1_ErrorEmptyName()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( 4 ) )
            .Returns( 1 )
            .Returns( 4 );
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "" );

        _gameManager.PlayGame();

        _consoleMock.Verify( c => c.WriteLine( "Имя не может быть пустым!" ), Times.Once );
        _factoryMock.Verify( f => f.CreateFighter( It.IsAny<string>() ), Times.Never );
    }

    [Fact]
    public void PlayGame_Choice_1_ValidName_AddFighter()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( 4 ) )
            .Returns( 1 )
            .Returns( 4 );
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "Fighter" );
        _factoryMock.Setup( f => f.CreateFighter( "Fighter" ) ).Returns( new Mock<IFighter>().Object );

        _gameManager.PlayGame();

        _factoryMock.Verify( f => f.CreateFighter( "Fighter" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( "Боец добавлен!" ), Times.Once );
    }

    [Fact]
    public void PlayGame_Choice_2_NoFighters_ShowsEmptyError()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( 4 ) )
            .Returns( 2 )
            .Returns( 4 );

        _gameManager.PlayGame();

        _consoleMock.Verify( c => c.WriteLine( "Нет бойцов!" ), Times.Once );
    }

    [Fact]
    public void PlayGame_Choice_2_ShowFighterName()
    {
        Mock<IFighter> fighterMock = new();
        fighterMock.Setup( f => f.Name ).Returns( "Fighter" );
        fighterMock.Setup( f => f.ToString() ).Returns( "info" );

        _inputHelperMock.SetupSequence( i => i.ReadChoice( 4 ) )
            .Returns( 1 )
            .Returns( 2 )
            .Returns( 4 );
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "Fighter" );
        _factoryMock.Setup( f => f.CreateFighter( "Fighter" ) ).Returns( fighterMock.Object );

        _gameManager.PlayGame();

        _consoleMock.Verify( c => c.WriteLine( "1. Имя: Fighter" ), Times.Once );
        _consoleMock.Verify( c => c.WriteLine( fighterMock.Object ), Times.Once );
    }

    [Fact]
    public void PlayGame_Choice_3_LessThanTwoFighters_ShowsError()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( 4 ) )
            .Returns( 3 )
            .Returns( 4 );

        _gameManager.PlayGame();

        _consoleMock.Verify( c => c.WriteLine( "Для битвы нужно минимум 2 бойца!" ), Times.Once );
        _battleManagerMock.Verify( b => b.StartBattle( It.IsAny<List<IFighter>>() ), Times.Never );
    }
}
