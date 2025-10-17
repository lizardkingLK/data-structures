using static dataStructures.Core.Linear.Arrays.Shared.Constants;
using static dataStructures.Core.Linear.Arrays.Shared.Exceptions;

namespace dataStructures.Core.Linear.Arrays;

public class DynamicArray<T>
{
    private const float GROWTH_FACTOR = .7f;
    private const float SHRINK_FACTOR = .3f;
    private int _capacity;

    private T?[] _values;

    public IEnumerable<T?> Values => GetValues();

    public T? this[int index]
    {
        get => GetValue(index);
        set => Update(index, value);
    }

    public int Size = 0;

    public DynamicArray(int capacity = INITIAL_CAPACITY)
    {
        if (capacity < INITIAL_CAPACITY)
        {
            throw InvalidListSizeException;
        }

        _capacity = capacity;
        _values = new T[capacity];
    }

    public DynamicArray(params T[] values) : this()
    {
        AddRange(values);
    }

    public DynamicArray(params DynamicArray<T>[] arrayList) : this()
    {
        foreach (DynamicArray<T> array in arrayList)
        {
            AddRange(array);
        }
    }

    public T Add(T value)
    {
        if ((float)Size / _capacity >= GROWTH_FACTOR)
        {
            GrowArray();
        }

        _values[Size++] = value;

        return value;
    }

    public void AddRange(T[] values)
    {
        foreach (T value in values)
        {
            Add(value);
        }
    }

    private void AddRange(DynamicArray<T> array)
    {
        foreach (T? value in array.Values)
        {
            if (value is not null)
            {
                Add(value);
            }
        }
    }

    public T Insert(int index, T value)
    {
        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        if ((float)Size / _capacity >= GROWTH_FACTOR)
        {
            GrowArray();
        }

        _values[index] = value;
        Size++;

        return value;
    }

    public T? Update(int index, T? value)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        _values[index] = value;

        return _values[index];
    }

    public bool TryUpdate(int index, T? value, out T? updated)
    {
        updated = default;

        if (Size == 0)
        {
            return false;
        }

        if (index < 0 || index > _capacity - 1)
        {
            return false;
        }

        updated = _values[index];
        _values[index] = value;

        return true;
    }

    public T? GetValue(int index)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        return _values[index];
    }

    public bool TryGetValue(int index, out T? value)
    {
        value = default;

        if (Size == 0 || index < 0 || index > _capacity - 1)
        {
            return false;
        }

        value = _values[index];

        return value is not null;
    }

    public bool TryGetValue(Predicate<T?> filterFunction, out T? value)
    {
        value = default;

        if (Size == 0)
        {
            return false;
        }

        foreach (T? item in _values)
        {
            if (filterFunction.Invoke(item))
            {
                value = item;
                return true;
            }
        }

        return false;
    }

    public T? Remove()
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        T? removed = _values[Size - 1];
        _values[--Size] = default;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Remove(T target)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (!TryGetValue(item => item != null && item.Equals(target), out T? removed))
        {
            throw ItemDoesNotExistException;
        }

        Size--;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return removed;
    }

    public bool TryRemove(T target, out T? removed)
    {
        removed = default;

        if (Size == 0)
        {
            return false;
        }

        if (!TryGetValue(item => item != null && item.Equals(target), out removed))
        {
            return false;
        }

        Size--;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return true;
    }

    public T? Remove(int index)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        T? value = _values[index];
        _values[index] = default;
        for (int i = index; i < Size - 1; i++)
        {
            _values[i] = _values[i + 1];
            _values[i + 1] = default;
        }

        Size--;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return value;
    }

    public bool TryRemove(int index, out T? removed)
    {
        removed = default;

        if (Size == 0)
        {
            return false;
        }

        if (index < 0 || index > _capacity - 1)
        {
            return false;
        }

        removed = _values[index];
        _values[index] = default;

        for (int i = index; i < Size - 1; i++)
        {
            _values[i] = _values[i + 1];
            _values[i + 1] = default;
        }

        Size--;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return true;
    }

    private IEnumerable<T?> GetValues()
    {
        foreach (T? value in _values)
        {
            yield return value;
        }
    }

    private void GrowArray()
    {
        int newCapacity = _capacity * 2;
        T?[] newValues = new T[newCapacity];
        for (int i = 0; i < _capacity; i++)
        {
            newValues[i] = _values[i];
        }

        _capacity = newCapacity;
        _values = newValues;
    }

    private void ShrinkArray()
    {
        int newCapacity = Size;
        if (newCapacity == 0)
        {
            newCapacity = INITIAL_CAPACITY;
        }

        T?[] newValues = new T[newCapacity];
        for (int i = 0; i < newCapacity; i++)
        {
            newValues[i] = _values[i];
        }

        _capacity = newCapacity;
        _values = newValues;
    }
}