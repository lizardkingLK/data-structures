using dataStructures.Core.Shared;

namespace dataStructures.Core.Linear.LinkedList;

public class ALinkedList<T>
{
    public LinkNode<T>? Head { get; set; }

    public void Display()
    {
        LinkNode<T>? current = Head;
        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Next;
        }
    }

    public void InsertToEnd(T value)
    {
        LinkNode<T>? current = Head;
        if (current == null)
        {
            Head = new(value);
            return;
        }

        while (current != null)
        {
            if (current.Next == null)
            {
                current.Next = new(value);
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
            Head = new(value);
            return;
        }

        Head = new(value)
        {
            Next = current
        };
    }

    public LinkNode<T>? RemoveFromEnd()
    {
        LinkNode<T>? parent = null;
        LinkNode<T>? current = Head;
        while (current?.Next != null)
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
        LinkNode<T>? parent = null;
        LinkNode<T>? current = Head;
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

    public T? Search(Func<T, bool> searchFunction)
    {
        LinkNode<T>? current = Head;
        T? value = default;
        while (current != null)
        {
            if (searchFunction(current.Value))
            {
                value = current.Value;
                break;
            }

            current = current.Next;
        }

        return value;
    }
}