using dataStructures.Core.Linear.Arrays;

namespace dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

public interface INotification<V>
{
    public V Value { get; init; }
    public DynamicArray<(string, object)>? Data { get; init; }
}