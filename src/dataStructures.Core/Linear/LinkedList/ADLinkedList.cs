using dataStructures.Core.Shared;

namespace dataStructures.Core.Linear.LinkedList;

public class ADLinkedList<T>
{
    public LinkNode<T>? Head { get; set; }

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
            Next = current,
        };

        current.Previous = Head;
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
                current.Next = new(value, null, current);
                break;
            }

            current = current.Next;
        }
    }

    public void InsertAfter(T value)
    {
        if (!FindValue(value, out LinkNode<T>? current))
        {
            return;
        }


    }

    public void DisplayForwardWay(T value)
    {
        Console.WriteLine("info. {0} from {1}", nameof(DisplayForwardWay), value);
        if (!FindValue(value, out LinkNode<T>? current))
        {
            return;
        }

        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Next;
        }
    }

    private bool FindValue(T? value, out LinkNode<T>? current)
    {
        current = Head;
        bool valueFound = false;
        while (current != null)
        {
            if (current.Value!.Equals(value))
            {
                valueFound = true;
                break;
            }

            current = current.Next;
        }

        if (!valueFound)
        {
            Console.WriteLine("error. value does not exist");
            return false;
        }

        return true;
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
        Head!.Previous = null;

        return current;
    }

    public LinkNode<T>? RemoveLinkNodeAtFirstOccurrence(T value)
    {
        if (!FindValue(value, out LinkNode<T>? current))
        {
            return default;
        }

        LinkNode<T>? previous = current!.Previous;
        previous!.Next = current.Next;
        current.Next!.Previous = previous;

        return current;
    }

    public void DisplayBackwardWay(T value)
    {
        Console.WriteLine("info. {0} from {1}", nameof(DisplayBackwardWay), value);
        if (!FindValue(value, out LinkNode<T>? current))
        {
            return;
        }

        while (current != null)
        {
            Console.WriteLine(current.Value);
            current = current.Previous;
        }
    }
}