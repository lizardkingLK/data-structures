using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.NonLinear.Heaps.Enums;
using dataStructures.Core.NonLinear.Heaps.Strategies;

namespace dataStructures.Core.NonLinear.Heaps;

public abstract class BinaryHeap<T> where T : IComparable<T>
{
    public DynamicArray<T> Values { get; set; } = new();

    public static BinaryHeap<T> Create(HeapTypeEnum heapType)
    => heapType == HeapTypeEnum.MinHeap
    ? new MinHeap<T>()
    : new MaxHeap<T>();

    public abstract void Insert(T value);

    // public abstract void Delete(T value);

    // public abstract int Search(T value);
}