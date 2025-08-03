namespace dataStructures.Core.Linear.Stack;

public class Stack<T>
{
    private int _top;

    private readonly int _size;

    private readonly T[] _values;

    public Stack(int size)
    {
        if (size <= 0)
        {
            throw new Exception("error. cannot create. invalid size");
        }

        _top = -1;
        _size = size;
        _values = new T[_size];
    }

    public T? Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek. stack is empty");
        }

        return _values[_top];
    }

    public T? Pop()
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

    private bool IsFull()
    {
        return _top == _size - 1;
    }

    private bool IsEmpty()
    {
        return _top == -1;
    }
}
