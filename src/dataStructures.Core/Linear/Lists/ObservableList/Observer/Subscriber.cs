using dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

namespace dataStructures.Core.Linear.Lists.ObservableList.Observer;

public class Subscriber<T>(Action<INotification<T>> listener) : ISubscriber<T> where T : notnull
{
    private readonly Action<INotification<T>> _listener = listener;

    public void Notify(INotification<T> notification) => _listener.Invoke(notification);
}