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
    public void ReadChoice_ValidInput_ReturnsNumber()
    {
        _consoleMock.Setup( c => c.ReadLine() ).Returns( "3" );

        int result = _inputHelper.ReadChoice( maxOption: 5 );

        Assert.Equal( 3, result );
    }

    [Theory]
    [InlineData( "0", 5, 1 )]
    [InlineData( "10", 3, 3 )]
    [InlineData( "", 5, 1 )]
    public void ReadChoice_InvalidInput_RetriesUntilValid( string invalidInput, int maxOption, int expected )
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( invalidInput )
            .Returns( expected.ToString() );

        int result = _inputHelper.ReadChoice( maxOption: maxOption );

        Assert.Equal( expected, result );
    }

    [Fact]
    public void ReadChoice_InvalidString_ShowsErrorMessage()
    {
        _consoleMock.SetupSequence( c => c.ReadLine() )
            .Returns( "abc" )
            .Returns( "2" );

        int result = _inputHelper.ReadChoice( maxOption: 3 );

        _consoleMock.Verify( c => c.WriteLine( "Введите число от 1 до 3" ), Times.Once );
        Assert.Equal( 2, result );
    }

}
