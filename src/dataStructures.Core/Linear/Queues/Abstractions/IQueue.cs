namespace dataStructures.Core.Linear.Queues.Abstractions;

public interface IQueue<T>
{
    void Insert(T item);
    T? Remove();
    T? Peek();
    bool IsEmpty();
    bool IsFull();
}