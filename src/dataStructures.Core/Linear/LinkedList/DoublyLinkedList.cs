using dataStructures.Core.Shared;

namespace dataStructures.Core.Linear.LinkedList;

public class DoublyLinkedList<T>
{
    private LinkNode<T>? _head;

    private LinkNode<T>? _tail;

    public LinkNode<T> AddToFront(T value)
    {
        LinkNode<T> newHead = new(null, value, null);
        if (_head == null)
        {
            _head = newHead;
            _tail = _head;
            return newHead;
        }

        _head.Previous = newHead;
        newHead.Next = _head;
        _head = newHead;

        return newHead;
    }

    public LinkNode<T> AddBefore(LinkNode<T> target, T value)
    {
        LinkNode<T>? current = _head
            ?? throw new Exception("error. cannot find node. list is empty");
        LinkNode<T> newNode = new(null, value, null);
        LinkNode<T>? previous;
        while (current != null)
        {
            if (!ReferenceEquals(current, target))
            {
                current = current.Next;
                continue;
            }

            previous = current.Previous;
            if (previous == null)
            {
                return AddToFront(value);
            }
            else
            {
                current.Previous = newNode;
                previous.Next = newNode;
                newNode.Previous = previous;
                newNode.Next = current;
            }

            return newNode;
        }

        return newNode;
    }

    public LinkNode<T> AddAfter(LinkNode<T> target, T value)
    {
        LinkNode<T>? current = _head
            ?? throw new Exception("error. cannot find node. list is empty");
        LinkNode<T> newNode = new(null, value, null);
        LinkNode<T>? next;
        while (current != null)
        {
            if (!ReferenceEquals(current, target))
            {
                current = current.Next;
                continue;
            }

            next = current.Next;
            if (next == null)
            {
                return AddToRear(value);
            }
            else
            {
                current.Next = newNode;
                next.Previous = newNode;
                newNode.Next = next;
                newNode.Previous = current;
            }

            return newNode;
        }

        return newNode;
    }

    public LinkNode<T> AddToRear(T value)
    {
        LinkNode<T> newTail = new(null, value, null);
        if (_tail == null)
        {
            _tail = newTail;
            _head = _tail;
            return newTail;
        }

        _tail.Next = newTail;
        newTail.Previous = _tail;
        _tail = newTail;

        return newTail;
    }

    public IEnumerable<T> ForwardTraversal()
    {
        LinkNode<T>? current = _head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    public IEnumerable<T> BackwardTraversal()
    {
        LinkNode<T>? current = _tail;
        while (current != null)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }

    public void Remove(T value)
    {
        LinkNode<T>? current = _head
            ?? throw new Exception("error. cannot remove. list is empty");
        LinkNode<T>? previous;
        LinkNode<T>? next;
        while (current != null)
        {
            previous = current.Previous;
            next = current.Next;
            if (!current.Value!.Equals(value))
            {
                current = next;
                continue;
            }

            if (previous == null)
            {
                _head = next;
            }
            else
            {
                previous.Next = next;
                current.Previous = null;
            }

            if (next == null)
            {
                _tail = previous;
            }
            else
            {
                next.Previous = previous;
                current.Next = null;
            }

            current = next;
        }
    }

    public void RemoveHead()
    {
        LinkNode<T>? head = _head
            ?? throw new Exception("error. cannot remove head. list is empty");
        LinkNode<T>? next = head.Next;
        if (next == null)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            head.Next = null;
            next.Previous = null;
            _head = next;
        }
    }

    public void RemoveTail()
    {
        LinkNode<T>? tail = _tail
            ?? throw new Exception("error. cannot remove tail. list is empty");
        LinkNode<T>? previous = tail.Previous;
        if (previous == null)
        {
            _tail = null;
            _head = null;
        }
        else
        {
            tail.Previous = null;
            previous.Next = null;
            _tail = previous;
        }
    }
}