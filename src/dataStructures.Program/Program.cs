using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.NonLinear.Graphs;
using dataStructures.Core.NonLinear.Graphs.Strategies;

namespace dataStructures.Program;

class Program
{
    static void TestAdjacencyList()
    {
        AdjacencyListGraph<string> g = Graph<string>.GetAdjacencyListGraph();

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
        foreach ((string neighbor, _) in g.GetNeighbors(whose).Values)
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

        Console.WriteLine($"\nall vertices are below. {nameof(g.BFSIterative)}");
        foreach (string neighbor in g.BFSIterative())
        {
            Console.WriteLine(neighbor);
        }

        AdjacencyListGraph<int> k = Graph<int>.GetAdjacencyListGraph();

        k.AddEdge(5, 3, 1, true);
        k.AddEdge(3, 1, 1, true);
        k.AddEdge(1, 2, 1, true);
        k.AddEdge(2, 6, 1, true);
        k.AddEdge(3, 4, 1, true);
        k.AddEdge(4, 2, 1, true);

        Console.WriteLine($"\ncycle is below. {nameof(g.FindCycleBFS)}");
        if (k.FindCycleBFS(5, 6, out DynamicArray<int>? cycle))
        {
            Console.WriteLine("cycle = {0}", string.Join('-', cycle!.Values));
        }

        Console.WriteLine($"\ncycle is below. {nameof(g.FindCycleDFS)}");
        if (k.FindCycleDFS(5, 6, out cycle))
        {
            Console.WriteLine("cycle = {0}", string.Join('-', cycle!.Values));
        }
    }

    static void TestAdjacencyMatrix()
    {
        AdjacencyMatrixGraph<string> g = Graph<string>.GetAdjacencyMatrixGraph(
            ["foo", "bar", "me", "you", "dad", "mom"]);

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
        foreach ((string neighbor, _) in g.GetNeighbors(whose).Values)
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

        Console.WriteLine($"\nall vertices are below. {nameof(g.BFSIterative)}");
        foreach (string neighbor in g.BFSIterative())
        {
            Console.WriteLine(neighbor);
        }

        AdjacencyListGraph<int> k = Graph<int>.GetAdjacencyListGraph();

        k.AddEdge(5, 3, 1, true);
        k.AddEdge(3, 1, 1, true);
        k.AddEdge(1, 2, 1, true);
        k.AddEdge(2, 6, 1, true);
        k.AddEdge(3, 4, 1, true);
        k.AddEdge(4, 2, 1, true);

        Console.WriteLine($"\ncycle is below. {nameof(g.FindCycleBFS)}");
        if (k.FindCycleBFS(5, 6, out DynamicArray<int>? cycle))
        {
            Console.WriteLine("cycle = {0}", string.Join('-', cycle!.Values));
        }

        Console.WriteLine($"\ncycle is below. {nameof(g.FindCycleDFS)}");
        if (k.FindCycleDFS(5, 6, out cycle))
        {
            Console.WriteLine("cycle = {0}", string.Join('-', cycle!.Values));
        }
    }

    static void Main()
    {
        // TestAdjacencyList();
        TestAdjacencyMatrix();
    }
}
