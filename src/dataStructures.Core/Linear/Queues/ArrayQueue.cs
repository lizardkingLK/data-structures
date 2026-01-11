using dataStructures.Core.Linear.Queues.Abstractions;

namespace dataStructures.Core.Linear.Queues;

public class ArrayQueue<T>(int capacity) : IQueue<T>
{
    private int _front;

    private int _size;

    private readonly T?[] _values = new T[capacity];

    public void Enqueue(T value)
    {
        if (IsFull())
        {
            throw new Exception("error. cannot insert queue is full");
        }

        int rear = (_size + _front) % capacity;
        _values[rear] = value;
        _size++;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot peek queue is empty");
        }

        return _values[_front]!;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new Exception("error. cannot remove queue is empty");
        }

        T dequeued = _values[_front]!;
        _values[_front] = default;
        _front = (_front + 1) % capacity;
        _size--;

        return dequeued;
    }

    public bool IsEmpty() => _size == 0;

    public bool IsFull() => capacity == _size;
}