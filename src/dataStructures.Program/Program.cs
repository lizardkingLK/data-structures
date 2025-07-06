using dataStructures.Core.Linear.Array;
using dataStructures.Core.Linear.LinkedList;
using dataStructures.Core.Linear.Queue;
using dataStructures.Core.NonLinear.HashMap;
using dataStructures.Core.Shared;

namespace dataStructures.Program;

class Program
{
    static void TestAQueue()
    {
        AQueue<int> queue = new(3);
        queue.Insert(1);
        queue.Insert(2);
        queue.Insert(3);
        queue.Insert(4);

        int peeked1 = queue.Peek();
        Console.WriteLine(peeked1);

        int removed1 = queue.Remove();
        Console.WriteLine(removed1);

        int peeked2 = queue.Peek();
        Console.WriteLine(peeked2);

        int removed2 = queue.Remove();
        Console.WriteLine(removed2);

        int peeked3 = queue.Peek();
        Console.WriteLine(peeked3);

        int removed3 = queue.Remove();
        Console.WriteLine(removed3);

        int peeked4 = queue.Peek();
        Console.WriteLine(peeked4);

        queue.Insert(5);
        int peeked5 = queue.Peek();
        Console.WriteLine(peeked5);
    }

    static void TestACQueue()
    {
        ACQueue<int> queue = new(5);

        queue.Insert(1);
        queue.Insert(2);
        queue.Insert(3);
        queue.Insert(4);
        queue.Insert(5);
        queue.Insert(6);
        queue.Insert(7);

        int peeked = queue.Peek();
        Console.WriteLine(peeked);

        int removed = queue.Remove();
        Console.WriteLine(removed);

        peeked = queue.Peek();
        Console.WriteLine(peeked);

        removed = queue.Remove();
        Console.WriteLine(removed);

        removed = queue.Remove();
        Console.WriteLine(removed);

        removed = queue.Remove();
        Console.WriteLine(removed);

        removed = queue.Remove();
        Console.WriteLine(removed);

        peeked = queue.Peek();
        Console.WriteLine(peeked);

        queue.Insert(8);

        peeked = queue.Peek();
        Console.WriteLine(peeked);
    }

    static void TestALinkedList()
    {
        ALinkedList<int> linkedList = new()
        {
            Head = new(1, null)
        };

        linkedList.InsertToEnd(2);
        linkedList.InsertToFront(3);

        linkedList.Display();

        LinkNode<int>? removed = linkedList.RemoveFromEnd();
        Console.WriteLine("removed from last is " + removed?.Value);

        linkedList.InsertToFront(2);

        linkedList.Display();

        removed = linkedList.RemoveFromFront();
        Console.WriteLine("removed from front is " + removed?.Value);

        linkedList.InsertToEnd(2);
        linkedList.InsertToFront(5);
        linkedList.InsertToEnd(7);
        linkedList.InsertToEnd(1);

        linkedList.Display();

        removed = linkedList.RemoveLinkNodeAtFirstOccurrence(1);
        Console.WriteLine("removed element is " + removed?.Value);

        linkedList.Display();

        removed = linkedList.RemoveLinkNodeAtFirstOccurrence(1);
        Console.WriteLine("removed element is " + removed?.Value);

        linkedList.Display();
    }

    static void TestADLinkedList()
    {
        ADLinkedList<int> linkedList = new();
        linkedList.InsertToFront(3);
        linkedList.InsertToFront(2);
        linkedList.InsertToFront(1);
        // linkedList.DisplayForwardWay(1);
        // linkedList.DisplayBackwardWay(3);

        linkedList.InsertToEnd(4);
        linkedList.InsertToEnd(5);
        linkedList.InsertToEnd(6);
        // linkedList.DisplayForwardWay(1);
        // linkedList.DisplayBackwardWay(6);

        linkedList.RemoveFromEnd();
        // linkedList.DisplayForwardWay(1);
        // linkedList.DisplayBackwardWay(5);

        linkedList.RemoveFromFront();
        linkedList.DisplayForwardWay(2);
        // linkedList.DisplayBackwardWay(5);

        linkedList.InsertToEnd(6);
        linkedList.RemoveLinkNodeAtFirstOccurrence(3);
        linkedList.DisplayForwardWay(2);
        // linkedList.DisplayBackwardWay(6);
    }

    private static void TestADArray()
    {
        ADArray<int> dArray = new(2);
        dArray.Add(0);
        Console.WriteLine(dArray.GetValue(0));

        dArray.Add(1);
        dArray.Add(2);
        dArray.Display();

        dArray.Add(4);
        dArray.Add(5);
        dArray.Display();

        dArray.Insert(2, 3);
        dArray.Display();

        dArray.Delete();
        dArray.Delete();
        dArray.Delete();
        dArray.Display();

        dArray.Insert(2, 3);
        dArray.Insert(2, 3);
        dArray.Insert(0, 3);
        dArray.Display();

        dArray.Remove(2);
        dArray.Display();

        dArray.Add(2);
        dArray.Display();

        dArray.Add(3);
        dArray.Display();

        dArray.Remove(0);
        dArray.Display();

        dArray.Remove(1);
        dArray.Remove(1);
        dArray.Display();

        dArray.Insert(0, -1);
        dArray.Display();

        dArray.Remove(0);
        dArray.Display();

        dArray.Add(4);
        dArray.Add(5);
        dArray.Add(6);
        dArray.Add(7);
        dArray.Add(8);
        dArray.Display();

        dArray.Delete();
        dArray.Delete();
        dArray.Delete();
        dArray.Delete();
        dArray.Delete();
        dArray.Delete();
        dArray.Display(true);
    }

    private static void TestAStack()
    {
        throw new NotImplementedException();
    }

    private static void TestAHashSet()
    {
        AHashMap<int, int> hashSet = new(2, .5f);
        hashSet.Insert(2, 11);
        hashSet.Insert(4, 22);
        hashSet.Display();

        hashSet.Insert(3, 33);
        hashSet.Display();

        hashSet.Insert(1, 0);
        hashSet.Display();

        hashSet.Insert(8, 44);
        hashSet.Insert(12, 122);
        hashSet.Display();

        hashSet.Remove(12);
        hashSet.Display();
    }

    static void Main()
    {
        TestAHashSet();
        // TestADArray();
        // TestAStack();
        // TestAQueue();
        // TestACQueue();
        // TestALinkedList();
        // TestADLinkedList();
    }

}
