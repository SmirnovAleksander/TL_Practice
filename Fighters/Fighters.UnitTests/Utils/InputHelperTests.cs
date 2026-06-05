using Fighters.Utils;
using Moq;

namespace Fighters.UnitTests.Utils;

public class InputHelperTests
{
    private readonly Mock<IConsole> _consoleMock;
    private readonly InputHelper _inputHelper;

    public InputHelperTests()
    {
        _consoleMock = new Mock<IConsole>();
        _inputHelper = new InputHelper( _consoleMock.Object );
    }

    [Fact]
    public void ReadChoice_ValidInput_ReturnNumber()
    {
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "3" );

        int result = _inputHelper.ReadChoice( 5 );

        Assert.Equal( 3, result );
    }

    [Fact]
    public void ReadChoice_NumberLess_Retry()
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( "0" )
            .Returns( "1" );

        int result = _inputHelper.ReadChoice( 5 );

        Assert.Equal( 1, result );
    }

    [Fact]
    public void ReadChoice_NumberMore_Retry()
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( "10" )
            .Returns( "3" );

        int result = _inputHelper.ReadChoice( 3 );

        Assert.Equal( 3, result );
    }

    [Fact]
    public void ReadChoice_EmptyString_Retry()
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( "" )
            .Returns( "1" );

        int result = _inputHelper.ReadChoice( 5 );

        Assert.Equal( 1, result );
    }

    [Fact]
    public void ReadChoice_InvalidString_Retry()
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( "abc" )
            .Returns( "2" );

        int result = _inputHelper.ReadChoice( 3 );

        _consoleMock.Verify( c => c.WriteLine( "Введите число от 1 до 3" ), Times.Once );
        Assert.Equal( 2, result );
    }

}
