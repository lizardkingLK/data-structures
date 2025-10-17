using static dataStructures.Core.Linear.Arrays.Shared.Constants;
using static dataStructures.Core.Linear.Arrays.Shared.Exceptions;

namespace dataStructures.Core.Linear.Arrays;

public class DynamicArray<T>
{
    private const float SHRINK_FACTOR = .3f;
    private const float GROWTH_FACTOR = .7f;
    private int _capacity;

    private T?[] _values;
    public int Capacity;

    public int Size => _size;
    public IEnumerable<T?> Values => GetValues();

    public T? this[int index]
    {
        get => GetValue(index);
        set => Update(index, value);
    }

    public DynamicArray(int capacity = INITIAL_CAPACITY)
    {
        if (capacity < INITIAL_CAPACITY)
        {
            throw InvalidCapacityException;
        }

        Capacity = capacity;
        _values = new T[capacity];
    }

    public DynamicArray(params T[] values)
    {
        int length = values.Length;
        Capacity = length * 2;
        _values = new T[Capacity];
        _size = length;
        for (int i = 0; i < length; i++)
        {
            _values[i] = values[i];
        }
    }

    public void Add(T value)
    {
        GrowArrayIfSatisfies();
        _values[_size++] = value;
    }

    public bool TryAdd(int index, T value)
    {
        if ((float)Size / _capacity >= GROWTH_FACTOR)
        {
            return false;
        }

        GrowArrayIfSatisfies();

        _values[_size++] = value;

        return true;
    }

    public void AddRange(params T[] values)
    {
        int newSize = _size + values.Length;
        if (newSize / Capacity < GROWTH_FACTOR)
        {
            foreach (T value in values)
            {
                Add(value);
            }

            return;
        }

        int newCapacity = newSize * 2;
        T?[] newValues = new T[newCapacity];
        int i = 0;
        while (i < _size)
        {
            newValues[i] = _values[i];
            i++;
        }

        if ((float)Size / _capacity >= GROWTH_FACTOR)
        {
            newValues[i] = values[i];
            i++;
        }

        Capacity = newCapacity;
        _size = newSize;
        _values = newValues;
    }

    public T Insert(int index, T? value)
    {
        if (index < 0 || index >= _size)
        {
            throw InvalidIndexException;
        }

        GrowArrayIfSatisfies();

        for (int i = _size; i > index; i--)
        {
            _values[i] = _values[i - 1];
        }

        _values[index] = value;
        _size++;

        return value!;
    }

    public void InsertRange(int index, params T[] values)
    {
        if (index < 0 || index >= _size)
        {
            throw InvalidIndexException;
        }

        int length = values.Length;
        int newSize = _size + length;
        int i;
        if (newSize / Capacity < GROWTH_FACTOR)
        {
            i = newSize - 1;
            while (i >= index + length)
            {
                _values[i] = _values[i - length];
                i--;
            }

            i = 0;
            while (i < length)
            {
                _values[index + i] = values[i];
                i++;
            }

            _size = newSize;
            return;
        }

        int newCapacity = newSize * 2;
        T?[] newValues = new T[newCapacity];
        i = 0;
        while (i < index)
        {
            newValues[i] = _values[i];
            i++;
        }

        i = 0;
        while (i < length)
        {
            newValues[index + i] = values[i];
            i++;
        }

        i = index + length;
        while (i < newSize)
        {
            newValues[i] = _values[i - length];
            i++;
        }

        Capacity = newCapacity;
        _size = newSize;
        _values = newValues;
    }

    public void Update(int index, T? newValue)
    {
        if (index < 0 || index >= _size)
        {
            throw InvalidIndexException;
        }

        _values[index] = newValue;
    }

    public bool TryUpdate(int index, T newValue)
    {
        if (index < 0 || index >= _size)
        {
            return false;
        }

        _values[index] = newValue;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Delete() => Remove(_size - 1);

    public T? Remove(int index)
    {
        if (index < 0 || index >= _size)
        {
            throw InvalidIndexException;
        }

        T? removed = _values[index];
        _values[index] = default;
        if (index == _size - 1)
        {
            _size--;
            ShrinkArrayIfSatisfies();
            return removed;
        }

        Size--;

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            _values[i - 1] = _values[i];
            _values[i] = default;
        }

        _size--;
        ShrinkArrayIfSatisfies();

        return removed;
    }

    public bool TryRemove(int index, out T? removed)
    {
        removed = default;

        if (index < 0 || index >= _size)
        {
            return false;
        }

        removed = _values[index];
        _values[index] = default;
        if (index == _size - 1)
        {
            _size--;
            ShrinkArrayIfSatisfies();

            return true;
        }

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            _values[i - 1] = _values[i];
            _values[i] = default;
        }

        _size--;
        ShrinkArrayIfSatisfies();

        return true;
    }

    public T? Remove(T value)
    {
        for (int i = 0; i < _size; i++)
        {
            if (value!.Equals(_values[i]))
            {
                Remove(i);
                return value;
            }
        }

        throw ValueNotFoundException;
    }

    public bool TryRemove(T value)
    {
        for (int i = 0; i < _size; i++)
        {
            if (value!.Equals(_values[i]))
            {
                TryRemove(i, out _);
                return true;
            }
        }

        return false;
    }

        if ((float)Size / _capacity <= SHRINK_FACTOR)
        {
            throw InvalidIndexException;
        }

        return _values[index];
    }

    public bool TryGetValue(int index, out T? value)
    {
        value = default;

        if (index < 0 || index >= _size)
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

    private void GrowArrayIfSatisfies()
    {
        if ((float)_size / Capacity < GROWTH_FACTOR)
        {
            return;
        }

        int newCapacity = Capacity * 2;
        T?[] newValues = new T[newCapacity];
        for (int i = 0; i < Capacity; i++)
        {
            newValues[i] = _values[i];
        }

        Capacity = newCapacity;
        _values = newValues;
    }

    private void ShrinkArrayIfSatisfies()
    {
        if ((float)_size / Capacity > SHRINK_FACTOR)
        {
            return;
        }

        T?[] newValues = new T[_size];
        for (int i = 0; i < _size; i++)
        {
            newValues[i] = _values[i];
        }

        Capacity = _size;
        _values = newValues;
    }
}