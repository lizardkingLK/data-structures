using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;
using dataStructures.Core.Linear.Lists.ObservableList.Abstractions;
using dataStructures.Core.Linear.Lists.ObservableList.Observer;

namespace dataStructures.Core.Linear.Lists.ObservableList;

public class ObservableList<T> : IObservableList<T> where T : notnull
{
    private readonly DynamicallyAllocatedArray<T> _list;

    private readonly Publisher<T> _publisher;

    public ObservableList(Action<INotification<T>> action)
    {
        _list = new();
        _publisher = new();
        _publisher.Subscribe(new Subscriber<T>(action));
    }

    public T? this[int index] { set => Update(index, value); }

    public T Add(T value)
    {
        _list.Add(value);

        return value;
    }

    public T Insert(int index, T value)
    {
        _list.Insert(index, value);

        return value;
    }

    public T? Remove()
    {
        T item = _list.Remove()!;

        return item;
    }

    public T? Remove(T target)
    {
        T item = _list.Remove(target)!;

        return item;
    }

    public bool TryRemove(T target, out T? removed)
    {
        removed = target;

        if (!_list.TryRemove(target, out _))
        {
            removed = default;
            return false;
        }

        return true;
    }

    public T? Remove(int index)
    {
        T item = _list.Remove(index)!;

        return item;
    }

    public bool TryRemove(int index, out T? removed)
    {
        bool couldRemove = _list.TryRemove(index, out removed);
        if (!couldRemove)
        {
            return false;
        }

        return true;
    }

    public T Update(int index, T? value)
    {
        _list.Update(index, value);

        return value!;
    }

    public bool TryUpdate(int index, T? value, out T? updated)
    {
        updated = value;

        bool couldUpdate = _list.TryUpdate(index, value!, out _);
        if (!couldUpdate)
        {
            updated = default;
            return false;
        }

        return true;
    }
}