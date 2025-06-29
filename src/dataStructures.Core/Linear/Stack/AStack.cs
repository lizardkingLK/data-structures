namespace dataStructures.Core.Linear.Stack;

public class AStack<T>(int size)
{
    private int top = -1;

    private readonly T[] values = new T[size];

    public T? Peek()
    {
        if (IsEmpty())
        {
            return default;
        }

        return values[top];
    }

    public T? Pop()
    {
        if (IsEmpty())
        {
            return default;
        }

        return values[top--];
    }

    public void Push(T value)
    {
        if (IsFull())
        {
            return;
        }

        values[++top] = value;
    }

    private bool IsFull()
    {
        return top == size - 1;
    }

    private bool IsEmpty()
    {
        return top == -1;
    }
}
