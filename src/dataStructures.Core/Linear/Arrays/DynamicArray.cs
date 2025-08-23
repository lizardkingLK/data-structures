using static dataStructures.Core.Linear.Arrays.Shared.Constants;

namespace dataStructures.Core.Linear.Arrays;

public class DynamicArray<T>
{
    public int Capacity { get; private set; }

    public int Size { get; private set; }

    public IEnumerable<T?> Values => ToList();

    private T?[] _values;

    public DynamicArray()
    {
        Capacity = 1;
        Size = 0;

        _values = new T[Capacity];
    }

    public DynamicArray(int capacity)
    {
        if (capacity <= 0)
        {
            throw new Exception("error. cannot create. invalid capacity");
        }

        Capacity = capacity;
        Size = 0;

        _values = new T[capacity];
    }

    public T? Add(T item)
    {
        if (Size == Capacity)
        {
            GrowArray();
        }

        _values[Size] = item;

        return _values[Size++];
    }

    public T? Add(int index, T item)
    {
        if (index < 0 || index >= Capacity)
        {
            throw new Exception("error. cannot insert. invalid index");
        }

        if (Size == Capacity)
        {
            GrowArray();
        }

        _values[index] = item;
        Size++;

        return _values[index];
    }

    public bool TryAdd(int index, T item)
    {
        if (index < 0 || index >= Capacity)
        {
            return false;
        }

        if (Size == Capacity)
        {
            GrowArray();
        }

        if (_values[index] is null)
        {
            _values[index] = item;
            Size++;
            return true;
        }

        return false; ;
    }

    public T? Remove()
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove. no items");
        }

        T? removed = _values[Size--];
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Remove(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove. no items");
        }

        if (index < 0 || index >= Capacity)
        {
            throw new Exception("error. cannot remove. invalid index");
        }

        T? removed = _values[index];
        int i;
        for (i = index; i < Size; i++)
        {
            _values[i] = _values[i + 1];
            _values[i + 1] = default;
        }

        Size--;
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Get(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot get. no items");
        }

        if (index < 0 || index >= Capacity)
        {
            throw new Exception("error. cannot get. invalid index");
        }

        return _values[index];
    }

    public bool TryGet(int index, out T? value)
    {
        value = default;

        if (Size == 0)
        {
            return false;
        }

        if (index < 0 || index >= Capacity)
        {
            return false;
        }

        value = _values[index];

        return value is not null;
    }

    private IEnumerable<T?> ToList()
    {
        foreach (T? value in _values)
        {
            yield return value;
        }
    }

    private void ShrinkArray()
    {
        int i;
        int newCapacity = Capacity / 3;
        if (newCapacity == 0)
        {
            newCapacity = INITIAL_CAPACITY;
        }

        T?[] tempValues = new T[newCapacity];
        for (i = 0; i < newCapacity; i++)
        {
            tempValues[i] = _values[i];
        }

        Capacity = newCapacity;
        _values = tempValues;
    }

    private void GrowArray()
    {
        int i;
        int newCapacity = Capacity * 2;
        T?[] tempValues = new T[newCapacity];
        for (i = 0; i < Size; i++)
        {
            tempValues[i] = _values[i];
        }

        Capacity = newCapacity;
        _values = tempValues;
    }
}