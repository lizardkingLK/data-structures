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
        void TestInitializationAction() => stack = new Core.Linear.Stacks.Stack<int>(LinkedListTyped, size);

        // Assert
        Exception exception = Assert.Throws<Exception>(TestInitializationAction);
        Assert.Equal("error. cannot create. invalid size", exception.Message);
        Assert.Null(stack);
    }
}