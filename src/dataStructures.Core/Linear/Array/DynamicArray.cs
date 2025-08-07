namespace dataStructures.Core.Linear.Array;

public class DynamicArray<T>
{
    public int Capacity { get; private set; }

    public int Size { get; private set; }

    private T[] _values;

    public IEnumerable<T> Values => GetValues();

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

    public void Add(T item)
    {
        if (Size == Capacity)
        {
            GrowArray();
        }

        _values[Size++] = item;
    }

    public void Add(int index, T item)
    {
        if (index < 0 || index > Size)
        {
            throw new Exception("error. cannot insert. invalid index");
        }

        if (Size == Capacity)
        {
            GrowArray();
        }

        int i;
        for (i = Size; i > index; i--)
        {
            _values[i] = _values[i - 1];
        }

        _values[index] = item;
        Size++;
    }

    public T Remove()
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove. no items");
        }

        T removed = _values[Size--];
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T Remove(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove. no items");
        }

        if (index < 0 || index >= Size)
        {
            throw new Exception("error. cannot insert. invalid index");
        }

        T removed = _values[index];
        int i;
        for (i = index; i < Size; i++)
        {
            _values[i] = _values[i + 1];
        }

        Size--;
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T Get(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot get. no items");
        }

        if (index < 0 || index >= Size)
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

        if (index < 0 || index >= Size)
        {
            return false;
        }

        value = _values[index];

        return true;
    }

    private IEnumerable<T> GetValues()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return _values[i];
        }
    }

    private void ShrinkArray()
    {
        int i;
        int newCapacity = Capacity / 3;
        T[] tempValues = new T[newCapacity];
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
        T[] tempValues = new T[newCapacity];
        for (i = 0; i < Size; i++)
        {
            tempValues[i] = _values[i];
        }

        Capacity = newCapacity;
        _values = tempValues;
    }
}