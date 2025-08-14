using dataStructures.Core.Linear.Stacks.Abstractions;
using static dataStructures.Core.Linear.Stacks.Enums.StackTypeEnum;

namespace dataStructures.Core.Tests.Linear.Stacks;

[Collection(nameof(TestArrayStack))]
public class TestArrayStack
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
        void TestInitializationAction() => stack = new Core.Linear.Stacks.Stack<int>(ArrayTyped, size);

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
        Core.Linear.Stacks.Stack<int> stack = new(size);

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
        Core.Linear.Stacks.Stack<int> stack = new(size);

        // Act
        int? popped = null;
        void TestPopAction() => popped = stack.Pop();

        // Assert
        Exception exception = Assert.Throws<Exception>(TestPopAction);
        Assert.Equal("error. cannot pop. stack is empty", exception.Message);
        Assert.Null(popped);
    }

    [Theory]
    [InlineData(5, 4, 53, 23, 45, 65)]
    [InlineData(6, 'a', 'b', 'c', 'd', 'e', 'f')]
    [InlineData(2, true, false)]
    [InlineData(4, "foobar", "nascar", "valdo", "kunda")]
    public void Should_Test_For_Stack_Push(int size, params object[] values)
    {
        // Arrange
        Core.Linear.Stacks.Stack<object> stack = new(size);

        // Act
        object? peeked;
        foreach (object value in values)
        {
            stack.Push(value);
            peeked = stack.Peek();

            // Assert
            Assert.Equal(value, peeked);
        }
    }

    [Theory]
    [InlineData(4, 4, 53, 23, 45, 65)]
    [InlineData(5, 'a', 'b', 'c', 'd', 'e', 'f')]
    [InlineData(1, true, false)]
    [InlineData(3, "foobar", "nascar", "valdo", "kunda")]
    public void Should_Test_For_Stack_Push_When_Full(int size, params object[] values)
    {
        // Arrange
        Core.Linear.Stacks.Stack<object> stack = new(size);

        // Act
        object? peeked = null;
        int i;
        for (i = 0; i < size; i++)
        {
            stack.Push(values[i]);
            peeked = stack.Peek();
        }

        void TestPushAction() => stack.Push(values[size]);

        // Assert
        Assert.Equal(values[i - 1], peeked);
        Exception exception = Assert.Throws<Exception>(TestPushAction);
        Assert.Equal("error. cannot push. stack is full", exception.Message);
    }

    [Theory]
    [InlineData(5, 4, 53, 23, 45, 65)]
    [InlineData(6, 'a', 'b', 'c', 'd', 'e', 'f')]
    [InlineData(2, true, false)]
    [InlineData(4, "foobar", "nascar", "valdo", "kunda")]
    public void Should_Test_For_Stack_Pop_All(int size, params object[] values)
    {
        // Arrange
        Core.Linear.Stacks.Stack<object> stack = new(size);

        // Act
        int i;
        for (i = 0; i < size; i++)
        {
            stack.Push(values[i]);
        }

        object? popped;
        for (i = size - 1; i >= 0; i--)
        {
            popped = stack.Pop();

            // Assert
            Assert.Equal(values[i], popped);
        }
    }

    [Fact]
    public void Should_Test_For_Stack_Push_Pop()
    {
        // Arrange
        Core.Linear.Stacks.Stack<int> stack = new(4);

        // Act
        stack.Push(1);
        _ = stack.Pop();

        stack.Push(452);
        int popped = stack.Pop();

        // Assert
        Assert.Equal(452, popped);
    }
}