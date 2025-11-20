namespace dataStructures.Core.NonLinear.Heaps.Strategies;

public class MinHeap<T> : BinaryHeap<T> where T : IComparable<T>
{
    internal MinHeap() { }

    public override void Insert(T value)
    {
        Values.Add(value);
        if (Values.Size == 1)
        {
            return;
        }

        HeapifyUp(Values.Size - 1);
    }

    public override void Update(T oldValue, T newValue)
    {
        int length = Values.Size;
        int i;
        for (i = 0; i < length; i++)
        {
            if (oldValue.Equals(Values[i]))
            {
                Values[i] = newValue;
                break;
            }
        }

        HeapifyUp(i);
    }

    public override T Delete()
    {
        if (Values.Size == 0)
        {
            throw new ApplicationException(
                "error. cannot delete. heap is empty");
        }

        SwapValues(0, Values.Size - 1);

        T deleted = Values.Delete()!;

        HeapifyDown(0);

        return deleted;
    }

    public override T Peek()
    {
        if (Values.Size == 0)
        {
            throw new ApplicationException(
                "error. cannot peek. heap is empty");
        }

        return Values[0]!;
    }

    private void HeapifyUp(int index)
    {
        T child = Values[index]!;

        int parentIndex = (index - 1) / 2;
        if (index == parentIndex)
        {
            return;
        }

        T? parent = Values[parentIndex];
        if (parent?.CompareTo(child) is < 1)
        {
            return;
        }

        SwapValues(parentIndex, index);

        HeapifyUp(parentIndex);
    }

    private void HeapifyDown(int index)
    {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        if (leftChildIndex >= Values.Size && rightChildIndex >= Values.Size)
        {
            return;
        }

        int minimumIndex = index;

        if (Values.TryGetValue(leftChildIndex, out T? leftChild)
        && Values[minimumIndex]!.CompareTo(leftChild) == 1)
        {
            minimumIndex = leftChildIndex;
        }

        if (Values.TryGetValue(rightChildIndex, out T? rightChild)
        && Values[minimumIndex]!.CompareTo(rightChild) == 1)
        {
            minimumIndex = rightChildIndex;
        }

        if (index == minimumIndex)
        {
            return;
        }

        SwapValues(index, minimumIndex);

        HeapifyDown(minimumIndex);
    }

    private void SwapValues(int indexA, int indexB)
    => (Values[indexA], Values[indexB]) = (Values[indexB], Values[indexA]);
}