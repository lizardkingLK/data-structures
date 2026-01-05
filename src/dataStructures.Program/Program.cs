using dataStructures.Core.NonLinear.Graphs;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        Graph<int> g = new();

        g.AddEdge(5, 3, true);
        g.AddEdge(3, 1, true);
        g.AddEdge(1, 2, true);
        g.AddEdge(2, 6, true);
        g.AddEdge(3, 4, true);
        g.AddEdge(4, 2, true);

        
    }
}
