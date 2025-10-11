namespace dataStructures.Core.Linear.Queues;

public class CircularQueue<T>(int size)
{
    private int front = 0;

    private int count = 0;

    private int rear = -1;

    private readonly T[] values = new T[size];

    public void Insert(T value)
    {
        if (IsFull())
        {
            throw new Exception("error. cannot insert queue is full");
        }

        rear = (front + count) % size;
        values[rear] = value;
        count++;
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

        T? removed = values[front];
        front = (front + 1) % size;
        count--;

        return removed;
    }

    private bool IsEmpty() => count == 0;

    private bool IsFull() => count == size;
}