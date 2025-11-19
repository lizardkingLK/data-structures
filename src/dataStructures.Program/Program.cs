using dataStructures.Core.NonLinear.Heaps;
using dataStructures.Core.NonLinear.Heaps.Enums;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        BinaryHeap<int> h1 = BinaryHeap<int>.Create(HeapTypeEnum.MinHeap);
        foreach (int _ in Enumerable.Range(0, 10))
        {
            h1.Insert(Random.Shared.Next(1, 10));
        }

        Console.WriteLine(string.Join(' ', h1.Values.Values));
    }
}
