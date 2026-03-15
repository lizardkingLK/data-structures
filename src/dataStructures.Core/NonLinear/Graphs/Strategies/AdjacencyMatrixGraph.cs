using System.Collections;
using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.Linear.Queues;
using dataStructures.Core.Linear.Queues.Enums;
using dataStructures.Core.Linear.Stacks.Enums;
using dataStructures.Core.NonLinear.Graphs.Enums;
using dataStructures.Core.NonLinear.HashMaps;

namespace dataStructures.Core.NonLinear.Graphs.Strategies;

public class AdjacencyMatrixGraph<T> : IEnumerable<T> where T : notnull
{
    private readonly int _size;
    private readonly T[] _vertices;
    private readonly HashMap<T, int> _verticeMap;
    private readonly double[,] _weights;

    public AdjacencyMatrixGraph(T[] vertices)
    {
        _size = vertices.Length;
        _vertices = new T[_size];
        _verticeMap = new();
        for (int i = 0; i < _size; i++)
        {
            _vertices[i] = vertices[i];
            _verticeMap.Add(_vertices[i], i);
        }

        _weights = new double[_size, _size];
        int length = _size * _size;
        int y;
        int x;
        for (int i = 0; i < length; i++)
        {
            (y, x) = (i / _size, i % _size);
            _weights[y, x] = y == x ? 0 : double.PositiveInfinity;
        }
    }

    public void AddEdge(T from, T to, double weight = 1, bool isUndirected = false)
    {
        if (!_verticeMap.ContainsKey(from) || !_verticeMap.ContainsKey(to))
        {
            throw new ApplicationException("error. given vertice(s) are invalid");
        }

        _weights[_verticeMap[from], _verticeMap[to]] = weight;
        if (isUndirected)
        {
            _weights[_verticeMap[to], _verticeMap[from]] = weight;
        }
    }

    public DynamicArray<(T, double)> GetNeighbors(T vertex)
    {
        DynamicArray<(T, double)> neighbors = new();

        int vertexIndex = _verticeMap[vertex];
        for (int i = 0; i < _size; i++)
        {
            if (i == vertexIndex)
            {
                continue;
            }

            if (_weights[vertexIndex, i] != double.PositiveInfinity)
            {
                neighbors.Add((_vertices[i], _weights[vertexIndex, i]));
            }
        }

        return neighbors;
    }

    public IEnumerable<T> DFSRecursive()
    {
        HashSet<T> visited = [];

        foreach (T vertice in _vertices)
        {
            foreach (T neighbor in DFSRecursive(vertice, visited))
            {
                yield return neighbor;
            }
        }
    }

    private IEnumerable<T> DFSRecursive(T current, HashSet<T> visited)
    {
        if (!visited.Add(current))
        {
            yield break;
        }

        yield return current;

        foreach ((T neighbor, double weight) in GetNeighbors(current).Values)
        {
            foreach (T vertex in DFSRecursive(neighbor, visited))
            {
                yield return vertex;
            }
        }
    }

    public IEnumerable<T> DFSIterative()
    {
        HashSet<T> visited = [];

        Linear.Stacks.Stack<T> values = new(StackTypeEnum.ArrayTyped, _size);
        T? popped;
        foreach (T vertex in _vertices)
        {
            values.Push(vertex);
            while (!values.IsEmpty())
            {
                popped = values.Pop();
                if (!visited.Add(popped))
                {
                    continue;
                }

                yield return popped;

                foreach ((T neighbor, _) in GetNeighbors(popped).Values)
                {
                    values.Push(neighbor);
                }
            }
        }
    }

    public IEnumerable<T> BFSIterative()
    {
        HashSet<T> visited = [];

        Linear.Queues.Queue<T> values = new(QueueTypeEnum.ArrayTyped, _size);
        T? dequeued;
        foreach (T vertex in _vertices)
        {
            values.Enqueue(vertex);
            while (!values.IsEmpty())
            {
                dequeued = values.Dequeue();
                if (!visited.Add(dequeued))
                {
                    continue;
                }

                yield return dequeued;

                foreach ((T neighbor, _) in GetNeighbors(dequeued).Values)
                {
                    values.Enqueue(neighbor);
                }
            }
        }
    }

    public bool FindCycleBFS(T from, T to, out DynamicArray<T>? cycle)
    {
        cycle = null;

        if (from!.Equals(to))
        {
            return false;
        }

        HashSet<T> visited = [];
        HashMap<T, T?> parents = new();
        Linear.Queues.Queue<T> values = new(QueueTypeEnum.ArrayTyped, _size);

        T? dequeued;

        visited.Add(from);
        values.Enqueue(from);
        parents.Add(from, default);

        bool cycleFound = false;
        while (!values.IsEmpty())
        {
            dequeued = values.Dequeue();
            if (dequeued!.Equals(to))
            {
                return cycleFound;
            }

            foreach ((T neighbor, double weight) in GetNeighbors(dequeued).Values)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    values.Enqueue(neighbor);
                    parents.Add(neighbor, dequeued);
                }
                else if (!parents[dequeued]!.Equals(neighbor))
                {
                    if (cycleFound)
                    {
                        continue;
                    }

                    cycle = GetCycle(parents, dequeued, neighbor);
                    cycleFound = true;
                }
            }
        }

        return true;
    }

    public bool FindCycleDFS(T from, T to, out DynamicArray<T>? cycle)
    {
        cycle = null;

        if (from!.Equals(to))
        {
            return false;
        }

        HashSet<T> visited = [];
        HashMap<T, T?> parents = new();
        Linear.Stacks.Stack<T> values = new(StackTypeEnum.ArrayTyped, _size);

        T? popped;

        visited.Add(from);
        values.Push(from);
        parents.Add(from, default);

        bool cycleFound = false;
        while (!values.IsEmpty())
        {
            popped = values.Pop();
            if (popped!.Equals(to))
            {
                return cycleFound;
            }

            foreach ((T neighbor, _) in GetNeighbors(popped).Values)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    values.Push(neighbor);
                    parents.Add(neighbor, popped);
                }
                else if (!parents[popped]!.Equals(neighbor))
                {
                    if (cycleFound)
                    {
                        continue;
                    }

                    cycle = GetCycle(parents, popped, neighbor);
                    cycleFound = true;
                }
            }
        }

        return true;
    }

    public bool FindShortestPath(
        T from,
        T to,
        out DynamicArray<T>? path,
        ShortestPathType type = ShortestPathType.Dijkstra)
    {
        try
        {
            return type switch
            {
                ShortestPathType.Dijkstra => FindShortestPathDijkstra(from, to, out path),
                ShortestPathType.BellmanFord => FindShortestPathBellmanFord(from, to, out path),
                _ => throw new NotImplementedException("error. algorithm not available"),
            };
        }
        catch (Exception e)
        {
            throw new Exception(
                string.Format("{0}. invalid for {1}",
                e.Message,
                type),
                e);
        }
    }

    private bool FindShortestPathBellmanFord(T from, T to, out DynamicArray<T>? path)
    {
        path = null;

        HashMap<T, T> parents = new();
        HashMap<T, double> distances = GetDistances();

        distances[from] = 0;

        int length = _vertices.Length;
        bool wasUpdated;
        for (int i = 0; i < length; i++)
        {
            wasUpdated = false;
            foreach (T vertexU in _vertices)
            {
                foreach ((T vertexV, double weight) in GetNeighbors(vertexU).Values)
                {
                    if (distances[vertexU] != double.PositiveInfinity
                    && distances[vertexU] + weight < distances[vertexV])
                    {
                        wasUpdated = true;
                        if (i == length - 1)
                        {
                            return false;
                        }

                        distances[vertexV] = distances[vertexU] + weight;
                        if (!parents.TryAdd(vertexV, vertexU))
                        {
                            parents[vertexV] = vertexU;
                        }
                    }
                }
            }

            if (!wasUpdated)
            {
                break;
            }
        }

        if (distances[to] == double.PositiveInfinity)
        {
            return false;
        }

        FindPath(to, parents, out path);

        return true;
    }

    private bool FindShortestPathDijkstra(T from, T to, out DynamicArray<T>? path)
    {
        path = null;

        HashMap<T, T> parents = new();
        HashSet<T> visited = [];
        HashMap<T, double> distances = GetDistances();

        T? current = from;
        double currentWeight = 0;
        PrioritizedQueue<double, T> queue = new();
        queue.Enqueue((currentWeight, current));
        while (!queue.IsEmpty())
        {
            (currentWeight, current) = queue.Dequeue();
            if (currentWeight < 0)
            {
                throw new ApplicationException(
                    "error. invalid weight in current context");
            }

            if (current.Equals(to))
            {
                break;
            }

            if (visited.Contains(current))
            {
                continue;
            }

            visited.Add(current);
            distances[current] = currentWeight;

            foreach ((T neighbor, double neighborWeight) in
            GetNeighbors(current).Values)
            {
                double newNeighborWeight = currentWeight + neighborWeight;
                if (newNeighborWeight > distances[neighbor])
                {
                    continue;
                }

                distances[neighbor] = newNeighborWeight;
                if (!parents.TryAdd(neighbor, current))
                {
                    parents[neighbor] = current;
                }

                queue.Enqueue((newNeighborWeight, neighbor));
            }
        }

        if (!current.Equals(to))
        {
            return false;
        }

        FindPath(to, parents, out path);

        return true;
    }

    private static void FindPath(
        T to,
        HashMap<T, T> parents,
        out DynamicArray<T>? path)
    {
        path = new();

        T? current = to;
        while (current != null)
        {
            path.Add(current);
            if (!parents.TryGet(current, out current))
            {
                break;
            }
        }

        path.Reverse();
    }

    private HashMap<T, double> GetDistances()
    {
        HashMap<T, double> distances = new();

        foreach (T vertex in _vertices)
        {
            distances.Add(vertex, double.PositiveInfinity);
        }

        return distances;
    }

    private static DynamicArray<T> GetCycle(HashMap<T, T?> parents, T current, T neighbor)
    {
        DynamicArray<T> currentToRoot = new();
        T? temp = current;
        while (temp != null && parents.ContainsKey(temp))
        {
            currentToRoot.Add(temp);
            temp = parents[temp];
        }

        currentToRoot.Reverse();

        DynamicArray<T> neighborToRoot = new();
        temp = neighbor;
        while (temp != null && parents.ContainsKey(temp))
        {
            neighborToRoot.Add(temp);
            temp = parents[temp];
        }

        neighborToRoot.Reverse();

        int length = Math.Min(currentToRoot.Size, neighborToRoot.Size);
        int commonIndex = 0;
        int i;
        for (i = 0; i < length; i++)
        {
            if (currentToRoot[i]!.Equals(neighborToRoot[i]))
            {
                commonIndex = i;
            }
            else
            {
                break;
            }
        }

        DynamicArray<T> cycle = new(currentToRoot[commonIndex]!);
        length = currentToRoot.Size;
        for (i = commonIndex + 1; i < length; i++)
        {
            cycle.Add(currentToRoot[i]!);
        }

        length = neighborToRoot.Size;
        for (i = commonIndex; i < length; i++)
        {
            cycle.Add(neighborToRoot[length - 1 - i]!);
        }


        return cycle;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T vertice in _vertices)
        {
            yield return vertice;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}