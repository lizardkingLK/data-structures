namespace dataStructures.Core.Linear.Stacks.Abstractions;

public interface IStack<T>
{
    void Push(T item);
    T Pop();
    T Peek();
    bool IsEmpty();
    bool IsFull();
}