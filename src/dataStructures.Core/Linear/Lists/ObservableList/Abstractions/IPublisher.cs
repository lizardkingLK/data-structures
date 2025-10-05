namespace dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

public interface IPublisher<T> where T : notnull
{
    void Subscribe(ISubscriber<T> subscriber);
    void Unsubscribe(ISubscriber<T> subscriber);
    void Publish(INotification<T> notification);
}