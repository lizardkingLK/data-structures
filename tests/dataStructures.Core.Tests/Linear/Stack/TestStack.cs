namespace dataStructures.Core.Tests.Linear.Stack;

[Collection(nameof(TestStack))]
public class TestStack
{
    [Theory]
    [InlineData(-4)]
    [InlineData(0)]
    [InlineData(-3)]
    public void Should_Test_For_Stack_Initilization_Error(int size)
    {
        // Arrange
        Core.Linear.Stack.Stack<int>? stack = null;

        // Act
        void TestInitializationAction() => stack = new(size);

        // Assert
        Exception exception = Assert.Throws<Exception>(TestInitializationAction);
        Assert.Equal("error. cannot create. invalid size", exception.Message);
        Assert.Null(stack);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(2)]
    [InlineData(9)]
    [InlineData(7)]
    [InlineData(4)]
    public void Should_Test_For_Stack_Peek(int size)
    {
        // Arrange
        Core.Linear.Stack.Stack<int> stack = new(size);

        // Act
        int? peeked = null;
        void TestPeekAction() => peeked = stack.Peek();

        // Assert
        Exception exception = Assert.Throws<Exception>(TestPeekAction);
        Assert.Equal("error. cannot peek. stack is empty", exception.Message);
        Assert.Null(peeked);
    }
}