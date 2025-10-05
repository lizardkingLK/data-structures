using dataStructures.Core.Linear.Lists.LinkedLists;
using dataStructures.Core.Linear.Lists.LinkedLists.State;

namespace dataStructures.Core.Tests.Linear.Lists;

[Collection(nameof(TestDoublyLinkedList))]
public class TestDoublyLinkedList
{
    [Fact]
    public void Should_Test_For_Remove_From_Head()
    {
        // Arrange
        DoublyLinkedList<int> list = new();
        list.AddToTail(1);
        list.AddToTail(2);
        list.AddToTail(3);

        // Act
        LinkNode<int> removed = list.Remove(1);
        LinkNode<int>? head = list.Head;
        LinkNode<int>? tail = list.Tail;

        // Assert
        Assert.Equal(1, removed.Value);
        Assert.NotNull(head);
        Assert.Equal(2, head.Value);
        Assert.NotNull(tail);
        Assert.Equal(3, tail.Value);
    }

    [Fact]
    public void Should_Test_For_Remove_From_Middle()
    {
        // Arrange
        DoublyLinkedList<int> list = new();
        list.AddToTail(1);
        list.AddToTail(2);
        list.AddToTail(3);

        // Act
        LinkNode<int> removed = list.Remove(2);
        LinkNode<int>? head = list.Head;
        LinkNode<int>? tail = list.Tail;

        // Assert
        Assert.Equal(2, removed.Value);
        Assert.NotNull(head);
        Assert.Equal(1, head.Value);
        Assert.NotNull(tail);
        Assert.Equal(3, tail.Value);
    }

    [Fact]
    public void Should_Test_For_Remove_From_Tail()
    {
        // Arrange
        DoublyLinkedList<int> list = new();
        list.AddToTail(1);
        list.AddToTail(2);
        list.AddToTail(3);

        // Act
        LinkNode<int> removed = list.Remove(3);
        LinkNode<int>? head = list.Head;
        LinkNode<int>? tail = list.Tail;

        // Assert
        Assert.Equal(3, removed.Value);
        Assert.NotNull(head);
        Assert.Equal(1, head.Value);
        Assert.NotNull(tail);
        Assert.Equal(2, tail.Value);
    }
}