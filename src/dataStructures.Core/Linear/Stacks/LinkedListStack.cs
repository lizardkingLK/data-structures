using dataStructures.Core.Linear.Stacks.Abstractions;

namespace dataStructures.Core.Linear.Stacks;

internal class LinkedListStack<T> : IStack<T>
{
    private int _count;

    private readonly int _size;

    private readonly Lists.LinkedLists.LinkedList<T> _linkedList;

    public LinkedListStack(int size)
    {
        if (size <= 0)
        {
            throw new Exception("error. cannot create. invalid size");
        }

        _size = size;
        _linkedList = new();
    }

    public bool IsEmpty()
    {
        return _linkedList.Head is null;
    }

    public bool IsFull()
    {
        return _count == _size;
    }

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

        _linkedList.InsertToFront(item);
        _count++;
    }
}