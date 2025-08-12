namespace dataStructures.Core.Linear.Lists.LinkedLists.State;

public record LinkNode<T>(LinkNode<T>? Previous, T Value, LinkNode<T>? Next)
{
    public LinkNode<T>? Previous { get; set; } = Previous;
    public T Value { get; set; } = Value;
    public LinkNode<T>? Next { get; set; } = Next;
}