using dataStructures.Core.Linear.Stacks.Abstractions;
using static dataStructures.Core.Linear.Stacks.Enums.StackTypeEnum;

namespace dataStructures.Core.Tests.Linear.Stacks;

[Collection(nameof(TestMonotonicStack))]
public class TestMonotonicStack
{
    [Theory]
    [InlineData(-4)]
    [InlineData(0)]
    [InlineData(-3)]
    public void Should_Test_For_Stack_Initilization_Error(int size)
    {
        // Arrange
        IStack<int>? stack = null;

        // Act
        void TestInitializationAction() => stack = new Core.Linear.Stacks.Stack<int>(MonotonicTyped, size);

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
        Core.Linear.Stacks.Stack<int> stack = new(MonotonicTyped, size);

        // Act
        int? peeked = null;
        void TestPeekAction() => peeked = stack.Peek();

        // Assert
        Exception exception = Assert.Throws<Exception>(TestPeekAction);
        Assert.Equal("error. cannot peek. stack is empty", exception.Message);
        Assert.Null(peeked);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(2)]
    [InlineData(9)]
    [InlineData(7)]
    [InlineData(4)]
    public void Should_Test_For_Stack_Pop(int size)
    {
        // Arrange
        Core.Linear.Stacks.Stack<int> stack = new(MonotonicTyped, size);

        // Act
        int? popped = null;
        void TestPopAction() => popped = stack.Pop();

        // Assert
        Exception exception = Assert.Throws<Exception>(TestPopAction);
        Assert.Equal("error. cannot pop. stack is empty", exception.Message);
        Assert.Null(popped);
    }

    [Theory]
    [InlineData(5, 65, 4, 53, 23, 45, 65)]
    [InlineData(6, 'e', 'a', 'b', 'f', 'c', 'd', 'e')]
    [InlineData(2, true, true, false, true)]
    [InlineData(4, "kunda", "foobar", "nascar", "valdo", "kunda")]
    public void Should_Test_For_Stack_Push(int size, object? expectedPeeked, params object[] values)
    {
        // Arrange
        Core.Linear.Stacks.Stack<object> stack = new(MonotonicTyped, size);

        // Act
        object? actualPeeked;
        foreach (object value in values)
        {
            stack.Push(value);
        }

        actualPeeked = stack.Peek();

        // Assert
        Assert.Equal(expectedPeeked, actualPeeked);
    }
}