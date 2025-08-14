using dataStructures.Core.Linear.Stacks.Abstractions;

namespace dataStructures.Core.Linear.Stacks;

internal class ArrayStack<T> : IStack<T>
{
    private int _top;

    private readonly int _size;

    private readonly T[] _values;

    public ArrayStack(int size)
    {
        if (size <= 0)
        {
            throw new Exception("error. cannot create. invalid size");
        }

        _top = -1;
        _size = size;
        _values = new T[_size];
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek. stack is empty");
        }

        return _values[_top];
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot pop. stack is empty");
        }

        return _values[_top--];
    }

    public void Push(T value)
    {
        if (IsFull())
        {
            throw new Exception("error. cannot push. stack is full");
        }

        _values[++_top] = value;
    }

    public bool IsFull() => _top == _size - 1;

    public bool IsEmpty() => _top == -1;
}
