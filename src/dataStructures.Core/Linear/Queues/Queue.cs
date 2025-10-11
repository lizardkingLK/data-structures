using dataStructures.Core.Linear.Queues.Abstractions;
using dataStructures.Core.Linear.Queues.Enums;
using static dataStructures.Core.Linear.Queues.Enums.QueueTypeEnum;

namespace dataStructures.Core.Linear.Queues;

public class Queue<T>(QueueTypeEnum queueType, int size) : IQueue<T>
{
    private readonly IQueue<T> _queue = queueType switch
    {
        ArrayTyped => new ArrayQueue<T>(size),
        LinkedListTyped => new LinkedListQueue<T>(size),
        _ => throw new NotImplementedException("error. cannot create. queue type not found"),
    };

    public int Size { get; init; }

    public Queue(int size) : this(ArrayTyped, size)
    {
        Size = size;
    }

    public void Insert(T item) => _queue.Insert(item);

    public bool IsEmpty() => _queue.IsEmpty();

    public bool IsFull() => _queue.IsFull();

    public T? Peek() => _queue.Peek();

    public T? Remove() => _queue.Remove();
}