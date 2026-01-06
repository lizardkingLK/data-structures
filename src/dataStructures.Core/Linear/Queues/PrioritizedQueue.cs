using System.Collections;
using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.Linear.Queues.Abstractions;

namespace dataStructures.Core.Linear.Queues;

public class PrioritizedQueue<K, V> :
IQueue<(K, V)>,
IEnumerable<(K, V)> where K : notnull, IComparable
{
    private DynamicArray<(K key, V value)> _values = new();

    public int Size => _values.Size;

    public PrioritizedQueue(params (K key, V value)[] values)
    {
        foreach ((K key, V value) item in values)
        {
            _values.Add(item);
        }

        Heapify(_values);
    }

    public void Enqueue((K, V) item)
    {
        _values.Add(item);
        HeapifyUp(_values, Size - 1);
    }

    public bool IsEmpty() => Size == 0;

    public bool IsFull() => false;

    public (K, V) Peek()
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot peek. queue is empty");
        }

        return _values[Size - 1];
    }

    public bool TryPeek(out (K Key, V Value)? item)
    {
        item = default;

        if (IsEmpty())
        {
            return false;
        }

        item = _values[Size - 1];

        return true;
    }

    public (K, V) Dequeue()
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot remove. queue is empty");
        }

        Swap(_values, 0, Size - 1);
        (K key, V value) removed = _values.Delete();
        HeapifyDown(_values, Size, 0);

        return removed;
    }

    public bool TryRemove(out (K Key, V Value)? item)
    {
        item = default;

        if (IsEmpty())
        {
            return false;
        }

        Swap(_values, 0, Size - 1);
        item = _values.Delete();
        HeapifyDown(_values, Size, 0);

        return true;
    }

    public IEnumerator<(K, V)> GetEnumerator()
    {
        DynamicArray<(K key, V value)> tempValues = new(Size);
        for (int i = 0; i < Size; i++)
        {
            tempValues.Add(_values[i]);
        }

        while (TryRemove(out (K Key, V Value)? item))
        {
            yield return item.GetValueOrDefault();
        }

        _values = tempValues;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static void Heapify(DynamicArray<(K key, V value)> values)
    {
        int length = values.Size;
        int index = length / 2 - 1;
        for (int i = index; i >= 0; i--)
        {
            HeapifyDown(values, length, i);
        }
    }

    private static void HeapifyUp(DynamicArray<(K Key, V Value)> values, in int index)
    {
        int parentIndex = (index - 1) / 2;
        if (parentIndex == index)
        {
            return;
        }

        if (values[parentIndex].Key.CompareTo(values[index].Key) < 1)
        {
            return;
        }

        Swap(values, parentIndex, index);

        HeapifyUp(values, parentIndex);
    }

    private static void HeapifyDown(
        DynamicArray<(K Key, V Value)> values,
        in int length,
        in int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        if (leftChildIndex >= length && rightChildIndex >= length)
        {
            return;
        }

        int minimumIndex = index;
        if (leftChildIndex < length
        && values[minimumIndex].Key.CompareTo(values[leftChildIndex].Key) == 1)
        {
            minimumIndex = leftChildIndex;
        }

        if (rightChildIndex < length
        && values[minimumIndex].Key.CompareTo(values[rightChildIndex].Key) == 1)
        {
            minimumIndex = rightChildIndex;
        }

        if (index == minimumIndex)
        {
            return;
        }

        Swap(values, index, minimumIndex);

        HeapifyDown(values, length, minimumIndex);
    }

    private static void Swap(DynamicArray<(K Key, V Value)> values, int indexA, int indexB)
    => (values[indexA], values[indexB]) = (values[indexB], values[indexA]);
}