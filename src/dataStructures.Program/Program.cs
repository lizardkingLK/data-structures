using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.NonLinear.Graphs;
using dataStructures.Core.NonLinear.Graphs.Strategies;
using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Program;

class Program
{
    static void TestAdjacencyList()
    {
        AdjacencyListGraph<string> g = Graph<string>.CreateAdjacencyListGraph();

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

        AdjacencyListGraph<int> k = Graph<int>.CreateAdjacencyListGraph();

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

        AdjacencyListGraph<string> l = Graph<string>.CreateAdjacencyListGraph();

        l.AddEdge("a", "c", 35);
        l.AddEdge("c", "f", 30);
        l.AddEdge("c", "e", 30);
        l.AddEdge("e", "d", 45);
        l.AddEdge("a", "b", 5);
        l.AddEdge("b", "e", 25);
        l.AddEdge("b", "d", 20);
        l.AddEdge("e", "f", 25);
        l.AddEdge("a", "d", 40);
        l.AddEdge("d", "f", 20);

        if (l.FindShortestPath("a", "f", out DynamicArray<string>? path0))
        {
            Console.WriteLine("\nshortest path = {0}", string.Join('-', path0!.Values));
        }

        k = Graph<int>.CreateAdjacencyListGraph();

        // k.AddEdge(0, 1, 5);
        // k.AddEdge(1, 2, 1);
        // k.AddEdge(1, 3, 2);
        // k.AddEdge(2, 4, 1);
        // k.AddEdge(4, 3, -1);

        k.AddEdge(0, 1, 4);
        k.AddEdge(1, 2, -6);
        k.AddEdge(2, 3, 5);
        k.AddEdge(3, 1, -2);

        if (k.FindShortestPath(0, 3, out DynamicArray<int>? path1, Core.NonLinear.Graphs.Enums.ShortestPathType.BellmanFord))
        {
            Console.WriteLine("\nshortest path = {0}", string.Join('-', path1!.Values));
        }
    }

    static void TestAdjacencyMatrix()
    {
        AdjacencyMatrixGraph<string> g = Graph<string>.CreateAdjacencyMatrixGraph(
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

        AdjacencyMatrixGraph<int> k = Graph<int>.CreateAdjacencyMatrixGraph(
            [1, 2, 3, 4, 5, 6]);

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

        AdjacencyMatrixGraph<string> l = Graph<string>.CreateAdjacencyMatrixGraph(
            ["a", "b", "c", "d", "e", "f"]);

        l.AddEdge("a", "c", 35);
        l.AddEdge("c", "f", 30);
        l.AddEdge("c", "e", 30);
        l.AddEdge("e", "d", 45);
        l.AddEdge("a", "b", 5);
        l.AddEdge("b", "e", 25);
        l.AddEdge("b", "d", 20);
        l.AddEdge("e", "f", 25);
        l.AddEdge("a", "d", 40);
        l.AddEdge("d", "f", 20);

        if (l.FindShortestPath("a", "f", out DynamicArray<string>? path))
        {
            Console.WriteLine("\nshortest path = {0}", string.Join('-', path!.Values));
        }

        k = Graph<int>.CreateAdjacencyMatrixGraph(
            [0, 1, 2, 3, 4]);

        k.AddEdge(0, 1, 5);
        k.AddEdge(1, 2, 1);
        k.AddEdge(1, 3, 2);
        k.AddEdge(2, 4, 1);
        k.AddEdge(4, 3, -1);

        // if (k.FindShortestPath(0, 3, out DynamicArray<int>? path0, Core.NonLinear.Graphs.Enums.ShortestPathType.BellmanFord))
        // {
        //     Console.WriteLine("\nshortest path = {0}", string.Join('-', path0!.Values));
        // }

        k = Graph<int>.CreateAdjacencyMatrixGraph(
            [0, 1, 2, 3]);

        k.AddEdge(0, 1, 4);
        k.AddEdge(1, 2, -6);
        k.AddEdge(2, 3, 5);
        k.AddEdge(3, 1, -2);

        if (k.FindShortestPath(0, 3, out DynamicArray<int>? path1, Core.NonLinear.Graphs.Enums.ShortestPathType.BellmanFord))
        {
            Console.WriteLine("\nshortest path = {0}", string.Join('-', path1!.Values));
        }
    }

    static void TestQuadTree()
    {
        char[][] map = [
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', 'x', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'x', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', 'x', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', 'x', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'x', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'x', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', 'x', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
        ];

        (int height, int width) = (map.Length, map[0].Length);
        QuadTree<int> qt = new(new(0, 0), new(height - 1, width - 1));
        int length = height * width;
        for (int i = 0; i < length; i++)
        {
            (int y, int x) = (i / width, i % width);
            if (map[y][x] == 'x')
            {
                qt.Insert((y, x, i));
            }
        }

        // QuadTree<int>.Point point = new(4, 3);
        QuadTree<int>.Point point = new(3, 3);
        QuadTree<int>.Node? lookup = qt.Search(point);
        if (lookup == null)
        {
            return;
        }

        Console.WriteLine(lookup);
    }

    static void Main()
    {
        // TestAdjacencyList();
        // TestAdjacencyMatrix();
        TestQuadTree();
    }
}
