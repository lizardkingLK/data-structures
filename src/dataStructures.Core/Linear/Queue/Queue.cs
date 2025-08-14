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
            throw new Exception("error. cannot insert queue is full");
        }

        values[++rear] = value;
    }

    public T? Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek queue is empty");
        }

        return values[front];
    }

    public T? Remove()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot remove queue is empty");
        }

        return values[front++];
    }

    private bool IsEmpty() => rear == -1 || front == size;

    private bool IsFull() => rear == size - 1;
}