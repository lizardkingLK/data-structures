using dataStructures.Core.Linear.Queues;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        var pq1 = new PrioritizedQueue<int, int>(
            (12, 12),
            (90, 90),
            (34, 34),
            (34, 34),
            (44, 44),
            (12, 12),
            (45, 45),
            (3, 3),
            (2, 2),
            (45, 45),
            (-23, -23),
            (43, 43),
            (0, 0),
            (234, 234)
        );
        Console.WriteLine(string.Join(' ', pq1));
        //
        Console.WriteLine(string.Join(' ', pq1));
        //
        while (pq1.TryRemove(out (int Key, int Value)? removed))
        {
            Console.WriteLine(removed);
            Console.WriteLine(string.Join(' ', pq1));
        }
    }
}
