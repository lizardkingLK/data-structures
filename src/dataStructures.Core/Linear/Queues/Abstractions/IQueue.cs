namespace dataStructures.Core.Linear.Queues.Abstractions;

public interface IQueue<T>
{
    void Enqueue(T item);
    T Dequeue();
    T Peek();
    bool IsEmpty();
    bool IsFull();
}