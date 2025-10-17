using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;
using dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

namespace dataStructures.Core.Linear.Lists.ObservableList.Observer;

public class Publisher<T> : IPublisher<T> where T : notnull
{
    private DynamicallyAllocatedArray<ISubscriber<T>> Subscribers { get; init; } = new();

    public void Publish(INotification<T> notification)
    {
        foreach (Subscriber<T>? subscriber in Subscribers.Values.Cast<Subscriber<T>?>())
        {
            subscriber?.Notify(notification);
        }
    }

    public void Subscribe(ISubscriber<T> subscriber) => Subscribers.Add(subscriber);

    public void Unsubscribe(ISubscriber<T> subscriber) => Subscribers.Remove(subscriber);
}