using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;

namespace dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

public interface INotification<V>
{
    public V Value { get; init; }
    public DynamicallyAllocatedArray<(string, object)>? Data { get; init; }
}