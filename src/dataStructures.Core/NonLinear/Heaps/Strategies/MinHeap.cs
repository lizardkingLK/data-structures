namespace dataStructures.Core.NonLinear.Heaps.Strategies;

public class MinHeap<T> : BinaryHeap<T> where T : IComparable<T>
{
    /*
                    1
            2               3
        4       5       6       7

        [1   2  3   4   5   6   7]
    */
    internal MinHeap() { }

    public override void Insert(T value)
    {
        Values.Add(value);
        Heapify();
    }

    private void Heapify()
    {
        throw new NotImplementedException();
    }
}