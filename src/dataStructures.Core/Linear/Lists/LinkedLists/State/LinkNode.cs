namespace dataStructures.Core.Linear.Lists.LinkedLists.State;

public record LinkNode<T>
{
    public LinkNode<T>? Previous { get; set; }

    public T Value { get; set; }

    public LinkNode<T>? Next { get; set; }

    public LinkNode(T value) : this(null, value, null)
    {
    }

    public LinkNode(LinkNode<T>? previous, T value, LinkNode<T>? next)
    {
        Previous = previous;
        Value = value;
        Next = next;
    }
}