namespace dataStructures.Core.Linear.Queue;

public class AQueue<T>(int size)
{
    private int front = 0;

    private int rear = -1;

    private readonly T[] values = new T[size];

    public void Insert(T value)
    {
        if (IsFull())
        {
            Console.WriteLine("error. cannot insert queue is full");
            return;
        }

        values[++rear] = value;
    }

    public T? Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("error. cannot peek queue is empty");
            return default;
        }

        return values[front];
    }

    public T? Remove()
    {
        if (IsEmpty())
        {
            Console.WriteLine("error. cannot remove queue is empty");
            return default;
        }

        return values[front++];
    }

    private bool IsEmpty()
    {
        return rear == -1 || front == size;
    }

    private bool IsFull()
    {
        return rear == size - 1;
    }
}