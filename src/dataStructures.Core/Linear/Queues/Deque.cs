using dataStructures.Core.Linear.Lists.LinkedLists.State;

namespace dataStructures.Core.Linear.Queues;

public class Deque<T>
{
    public LinkNode<T>? Head;
    public LinkNode<T>? Tail;
    public int Size { get; set; }

    public LinkNode<T> AddToHead(T value)
    {
        LinkNode<T> newNode = new(value);
        Size++;
        if (Head == null)
        {
            Head = newNode;
            if (Tail == null)
            {
                Tail = newNode;
            }

            return newNode;
        }

        newNode.Next = Head;
        Head.Previous = newNode;
        Head = newNode;

        return newNode;
    }

    public LinkNode<T> AddtoTail(T value)
    {
        LinkNode<T> newNode = new(value);
        Size++;
        if (Tail == null)
        {
            Tail = newNode;
            if (Head == null)
            {
                Head = newNode;
            }

            return newNode;
        }

        newNode.Previous = Tail;
        Tail.Next = newNode;
        Tail = newNode;

        return newNode;
    }

    public IEnumerable<T> GetValuesHeadToTail()
    {
        LinkNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    public IEnumerable<T> GetValuesTailToHead()
    {
        LinkNode<T>? current = Tail;
        while (current != null)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }

    public T RemoveFromFront()
    {
        LinkNode<T> headNode = Head
            ?? throw new Exception("error. cannot remove from front. deque is empty");
        if (headNode.Next != null)
        {
            headNode.Next.Previous = null;
        }

        Head = headNode.Next;
        headNode.Next = null;
        Size--;

        return headNode.Value;
    }

    public T RemoveFromRear()
    {
        LinkNode<T> tailNode = Tail
            ?? throw new Exception("error. cannot remove from rear. deque is empty");
        if (tailNode.Previous != null)
        {
            tailNode.Previous.Next = null;
        }

        Tail = tailNode.Previous;
        tailNode.Previous = null;
        Size--;

        return tailNode.Value;
    }

    public T? SearchValue(int index)
    {
        LinkNode<T>? current = Head;
        int i = 0;
        while (current != null && i <= index)
        {
            current = current.Next;
            i++;
        }

        if (current == null)
        {
            return default;
        }

        return current.Value;
    }

    public T SeekFront()
    {
        LinkNode<T> headNode = Head
            ?? throw new Exception("error. cannot seek at front. deque is empty");

        return headNode.Value;
    }

    public T SeekRear()
    {
        LinkNode<T> tailNode = Tail
            ?? throw new Exception("error. cannot seek at rear. deque is empty");

        return tailNode.Value;
    }
}
