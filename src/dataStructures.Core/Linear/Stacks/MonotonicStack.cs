using dataStructures.Core.Linear.Stacks.Abstractions;

namespace dataStructures.Core.Linear.Stacks;

public class MonotonicStack<T> : IStack<T>
{
    private int _count;

    private readonly int _size;

    private readonly Lists.LinkedLists.LinkedList<T> _linkedList;

    public MonotonicStack(int size)
    {
        if (size <= 0)
        {
            throw new Exception("error. cannot create. invalid size");
        }

        _size = size;
        _linkedList = new();
    }

    public bool IsEmpty() => _linkedList.Head is null;

    public bool IsFull() => _count == _size;

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek. stack is empty");
        }

        return _linkedList.Head!.Value;
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot pop. stack is empty");
        }

        Lists.LinkedLists.State.LinkNode<T>? popped = _linkedList.RemoveFromFront();
        _count--;

        return popped!.Value;
    }

    public void Push(T item)
    {
        if (IsFull())
        {
            throw new Exception("error. cannot push. stack is full");
        }

        while (TryPeek(out T? peeked) && peeked is IComparable peekedComparable)
        {
            if (peekedComparable.CompareTo(item) == -1)
            {
                break;
            }

            _linkedList.RemoveFromFront();
        }

        _linkedList.InsertToFront(item);
        _count++;
    }

    private bool TryPeek(out T? peeked)
    {
        peeked = default;

        if (IsEmpty())
        {
            return false;
        }

        peeked = _linkedList.Head!.Value;

        return true;
    }
}