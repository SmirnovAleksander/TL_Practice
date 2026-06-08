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
    public void PlayGame_EmptyName_DoesNotCreateFighter()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( maxOption: 4 ) )
            .Returns( 1 )
            .Returns( 4 );
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "" );

        _gameManager.PlayGame();

        _factoryMock.Verify( f => f.CreateFighter( It.IsAny<string>() ), Times.Never );
    }

    [Fact]
    public void PlayGame_ValidName_CreatesFighter()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( maxOption: 4 ) )
            .Returns( 1 )
            .Returns( 4 );
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "Fighter" );
        _factoryMock.Setup( f => f.CreateFighter( "Fighter" ) ).Returns( new Mock<IFighter>().Object );

        _gameManager.PlayGame();

        _factoryMock.Verify( f => f.CreateFighter( "Fighter" ), Times.Once );
    }

    [Fact]
    public void PlayGame_LessThanTwoFighters_DoesNotStartBattle()
    {
        _inputHelperMock.SetupSequence( i => i.ReadChoice( maxOption: 4 ) )
            .Returns( 3 )
            .Returns( 4 );

        _gameManager.PlayGame();

        _battleManagerMock.Verify( b => b.StartBattle( It.IsAny<List<IFighter>>() ), Times.Never );
    }
}
