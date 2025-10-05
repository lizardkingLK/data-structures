namespace dataStructures.Core.Linear.Lists.ObservableList.Abstractions;

public interface ISubscriber<T> where T : notnull
{
    void Notify(INotification<T> notification);
}