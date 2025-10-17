using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;
using dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

namespace dataStructures.Core.Linear.Lists.ObservableList.Observer;

public record Notification<T>(T Value, DynamicallyAllocatedArray<(string, object)>? Data = null) : INotification<T>;