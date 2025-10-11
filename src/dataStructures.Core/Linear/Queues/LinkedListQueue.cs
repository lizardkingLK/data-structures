using dataStructures.Core.Linear.Lists.LinkedLists.State;
using dataStructures.Core.Linear.Queues.Abstractions;

namespace dataStructures.Core.Linear.Queues;

public class LinkedListQueue<T>(int size) : IQueue<T>
{
    private readonly Lists.LinkedLists.LinkedList<T> _list = new();

    private readonly int _size = size;

    private int _count = 0;

    public void Insert(T item)
    {
        if (IsFull())
        {
            throw new Exception("error. cannot insert queue is full");
        }

        _list.InsertToEnd(item);
        _count++;
    }

    public T? Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek queue is empty");
        }

        return _list.Head!.Value;
    }

    public T? Remove()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot remove queue is empty");
        }

        LinkNode<T>? removed = _list.RemoveFromFront();
        _count--;

        return removed!.Value;
    }

    public bool IsEmpty() => _count == 0;

    public bool IsFull() => _count == _size;
}