using dataStructures.Core.NonLinear.Graphs.Strategies;

namespace dataStructures.Core.NonLinear.Graphs;

public class Graph<T> where T : notnull
{
    public static AdjacencyListGraph<T> GetAdjacencyListGraph() => new();

    public static AdjacencyMatrixGraph<T> GetAdjacencyMatrixGraph(T[] vertices) => new(vertices);
}