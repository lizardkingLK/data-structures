using dataStructures.Core.NonLinear.Graphs.Strategies;

namespace dataStructures.Core.NonLinear.Graphs;

public class Graph<T> where T : notnull
{
    public static AdjacencyListGraph<T> CreateAdjacencyListGraph() => new();

    public static AdjacencyMatrixGraph<T> CreateAdjacencyMatrixGraph(T[] vertices) => new(vertices);
}