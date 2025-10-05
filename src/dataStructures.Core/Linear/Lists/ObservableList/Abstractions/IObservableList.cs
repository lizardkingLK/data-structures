namespace dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

public interface IObservableList<T>
{
    public T? this[int index] { set; }

    T Add(T value);
    T Insert(int index, T value);
    T? Update(int index, T? value);
    bool TryUpdate(int index, T? value, out T? updated);
    T? Remove();
    T? Remove(T target);
    bool TryRemove(T target, out T? removed);
    T? Remove(int index);
    bool TryRemove(int index, out T? removed);
}