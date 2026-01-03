using dataStructures.Core.NonLinear.Graphs;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        Graph<string> g = new();

        Console.WriteLine($"\nall vertices are below. {nameof(g.DFSRecursive)}");
        foreach (string neighbor in g.DFSRecursive())
        {
            Console.WriteLine(neighbor);
        }

        g.AddEdge("foo", "bar");
        g.AddEdge("me", "you");
        g.AddEdge("you", "me");
        g.AddEdge("me", "dad");
        g.AddEdge("dad", "me");
        g.AddEdge("me", "mom");
        g.AddEdge("mom", "me");
        g.AddEdge("dad", "mom");
        g.AddEdge("mom", "dad");

        string whose = "you";
        Console.WriteLine($"\nneighbors of {whose} are below.");
        foreach (string neighbor in g.GetNeighbors(whose))
        {
            Console.WriteLine(neighbor);
        }

        Console.WriteLine($"\nall vertices are below. {nameof(g.DFSRecursive)}");
        foreach (string neighbor in g.DFSRecursive())
        {
            Console.WriteLine(neighbor);
        }

        Console.WriteLine($"\nall vertices are below. {nameof(g.DFSIterative)}");
        foreach (string neighbor in g.DFSIterative())
        {
            Console.WriteLine(neighbor);
        }
    }
}
