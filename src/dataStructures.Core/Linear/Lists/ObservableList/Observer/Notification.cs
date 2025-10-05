using dataStructures.Core.Linear.Arrays;
using dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

namespace dataStructures.Core.Linear.Lists.ObservableList.Observer;

public record Notification<T>(T Value, DynamicArray<(string, object)>? Data = null) : INotification<T>;