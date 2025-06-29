namespace dataStructures.Core.Linear.Queue;

public class ACQueue<T>(int size)
{
    private int front = 0;

    private int count = 0;

    private int rear = -1;

    private readonly T[] values = new T[size];

    public void Insert(T value)
    {
        if (IsFull())
        {
            Console.WriteLine("error. cannot insert queue is full");
            return;
        }

        rear = (front + count) % size;
        values[rear] = value;
        count++;
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

        T? removed = values[front];
        front = (front + 1) % size;
        count--;

        return removed;
    }

    private bool IsEmpty()
    {
        return count == 0;
    }

    private bool IsFull()
    {
        return count == size;
    }
}