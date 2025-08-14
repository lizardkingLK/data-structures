using dataStructures.Core.Linear.Lists.LinkedLists.State;

namespace dataStructures.Core.Linear.Lists.LinkedLists;

public class LinkedList<T>
{
    public LinkNode<T>? Head { get; set; }

    public IEnumerable<T> Values => ForwardTraversal();

    public void InsertToEnd(T value)
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            Head = new(null, value, null);
            return;
        }

        while (current != null)
        {
            if (current.Next == null)
            {
                current.Next = new(null, value, null);
                break;
            }

            current = current.Next;
        }
    }

    public void InsertToFront(T value)
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            Head = new(null, value, null);
            return;
        }

        Head = new(null, value, current);
    }

    public LinkNode<T>? RemoveFromEnd()
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            return default;
        }

        LinkNode<T>? parent = null;
        while (current.Next != null)
        {
            parent = current;
            current = current.Next;
        }

        parent!.Next = null;

        return current;
    }

    public LinkNode<T>? RemoveFromFront()
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            return default;
        }

        Head = current.Next;

        return current;
    }

    public LinkNode<T>? RemoveLinkNodeAtFirstOccurrence(T value)
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            return default;
        }

        if (current.Value!.Equals(value))
        {
            Head = Head!.Next;
            return current;
        }

        LinkNode<T>? parent = null;
        while (current != null)
        {
            if (current.Value!.Equals(value))
            {
                parent!.Next = current.Next;
                break;
            }

            parent = current;
            current = current.Next;
        }

        return current;
    }

    public bool Search(Func<T, bool> searchFunction, out T? value)
    {
        bool foundValue = false;

        value = default;
        LinkNode<T>? current = Head;
        while (current != null)
        {
            if (searchFunction(current.Value))
            {
                value = current.Value;
                foundValue = true;
                break;
            }

            current = current.Next;
        }

        return foundValue;
    }

    private IEnumerable<T> ForwardTraversal()
    {
        LinkNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }
}