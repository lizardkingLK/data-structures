using System.Collections;
using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.Linear.Queues.Enums;
using dataStructures.Core.Linear.Stacks.Enums;
using dataStructures.Core.NonLinear.HashMaps;

namespace dataStructures.Core.NonLinear.Graphs.Strategies;

public class AdjacencyListGraph<T> : IEnumerable<T> where T : notnull
{
    private readonly HashMap<T, DynamicArray<(T Neighbor, double Weight)>> _adjacencyList = new();

    public void AddVertex(T vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
        {
            _adjacencyList.Add(vertex, new());
        }
    }

    public void AddEdge(T from, T to, double weight = 1, bool undirected = false)
    {
        AddVertex(from);
        AddVertex(to);

        _adjacencyList[from].Add((to, weight));
        if (undirected)
        {
            _adjacencyList[to].Add((from, weight));
        }
    }

    public DynamicArray<(T, double)> GetNeighbors(T vertex)
    {
        if (_adjacencyList.TryGet(vertex, out DynamicArray<(T, double)>? neighbors))
        {
            return neighbors!;
        }

        return new();
    }

    public IEnumerable<T> DFSRecursive()
    {
        HashSet<T> visited = [];

        foreach ((T vertex, _) in _adjacencyList.GetKeyValues())
        {
            foreach (T neighbor in DFSRecursive(vertex, visited))
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

        Linear.Stacks.Stack<T> values = new(StackTypeEnum.ArrayTyped, _adjacencyList.Size);
        T? popped;
        foreach ((T vertex, _) in _adjacencyList.GetKeyValues())
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

        Linear.Queues.Queue<T> values = new(QueueTypeEnum.ArrayTyped, _adjacencyList.Size);
        T? dequeued;
        foreach ((T vertex, _) in _adjacencyList.GetKeyValues())
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
        Linear.Queues.Queue<T> values = new(QueueTypeEnum.ArrayTyped, _adjacencyList.Size);

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
        Linear.Stacks.Stack<T> values = new(StackTypeEnum.ArrayTyped, _adjacencyList.Size);

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

            foreach ((T neighbor, double weight) in GetNeighbors(popped).Values)
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
            cycle.Add(neighborToRoot[length - i]!);
        }

        return cycle;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach ((T vertice, _) in _adjacencyList.GetKeyValues())
        {
            yield return vertice;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}