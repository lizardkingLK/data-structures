using System.Collections;
using dataStructures.Core.Linear.Queues.Enums;
using dataStructures.Core.Linear.Stacks.Enums;
using dataStructures.Core.NonLinear.HashMaps;

namespace dataStructures.Core.NonLinear.Graphs;

public class Graph<T> : IEnumerable<T>
{
    private readonly HashMap<T, List<T>> _adjacencyList = new();

    public void AddVector(T vector)
    {
        if (!_adjacencyList.ContainsKey(vector))
        {
            _adjacencyList.Add(vector, []);
        }
    }

    public void AddEdge(T from, T to, bool undirected = false)
    {
        AddVector(from);
        AddVector(to);

        _adjacencyList[from].Add(to);
        if (undirected)
        {
            _adjacencyList[to].Add(from);
        }
    }

    public List<T> GetNeighbors(T source)
    {
        if (_adjacencyList.TryGet(source, out List<T>? neighbors))
        {
            return neighbors!;
        }

        return [];
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

        foreach (T neighbor in GetNeighbors(current))
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

                foreach (T neighbor in GetNeighbors(popped))
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

                foreach (T neighbor in GetNeighbors(dequeued))
                {
                    values.Enqueue(neighbor);
                }
            }
        }
    }

    public bool FindCycleBFS(T from, T to, out List<T>? cycle)
    {
        cycle = null;

        if (from!.Equals(to))
        {
            return false;
        }

        HashSet<T> visited = [];
        HashMap<T, T?> parentTracker = new();
        Linear.Queues.Queue<T> values = new(QueueTypeEnum.ArrayTyped, _adjacencyList.Size);

        T? dequeued;

        visited.Add(from);
        values.Enqueue(from);
        parentTracker.Add(from, default);

        bool cycleFound = false;
        while (!values.IsEmpty())
        {
            dequeued = values.Dequeue();
            if (dequeued!.Equals(to))
            {
                return cycleFound;
            }

            foreach (T neighbor in GetNeighbors(dequeued))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    values.Enqueue(neighbor);
                    parentTracker.Add(neighbor, dequeued);
                }
                else if (!parentTracker[dequeued]!.Equals(neighbor))
                {
                    if (cycleFound)
                    {
                        continue;
                    }

                    cycle = GetCycle(parentTracker, dequeued, neighbor);
                    cycleFound = true;
                }
            }
        }

        return true;
    }

    public bool FindCycleDFS(T from, T to, out List<T>? cycle)
    {
        cycle = null;

        if (from!.Equals(to))
        {
            return false;
        }

        HashSet<T> visited = [];
        HashMap<T, T?> parentTracker = new();
        Linear.Stacks.Stack<T> values = new(StackTypeEnum.ArrayTyped, _adjacencyList.Size);

        T? popped;

        visited.Add(from);
        values.Push(from);
        parentTracker.Add(from, default);

        bool cycleFound = false;
        while (!values.IsEmpty())
        {
            popped = values.Pop();
            if (popped!.Equals(to))
            {
                return cycleFound;
            }

            foreach (T neighbor in GetNeighbors(popped))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    values.Push(neighbor);
                    parentTracker.Add(neighbor, popped);
                }
                else if (!parentTracker[popped]!.Equals(neighbor))
                {
                    if (cycleFound)
                    {
                        continue;
                    }

                    cycle = GetCycle(parentTracker, popped, neighbor);
                    cycleFound = true;
                }
            }
        }

        return true;
    }

    private static List<T> GetCycle(HashMap<T, T?> parentTracker, T current, T neighbor)
    {
        List<T> currentToRoot = [];
        T? temp = current;
        while (temp != null && parentTracker.ContainsKey(temp))
        {
            currentToRoot.Add(temp);
            temp = parentTracker[temp];
        }

        currentToRoot.Reverse();

        List<T> neighborToRoot = [];
        temp = neighbor;
        while (temp != null && parentTracker.ContainsKey(temp))
        {
            neighborToRoot.Add(temp);
            temp = parentTracker[temp];
        }

        neighborToRoot.Reverse();

        int length = Math.Min(currentToRoot.Count, neighborToRoot.Count);
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

        List<T> cycle = [currentToRoot[commonIndex]];
        length = currentToRoot.Count;
        for (i = commonIndex + 1; i < length; i++)
        {
            cycle.Add(currentToRoot[i]);
        }

        length = neighborToRoot.Count;
        for (i = commonIndex; i < length; i++)
        {
            cycle.Add(neighborToRoot[length - i]);
        }

        return cycle;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach ((T vector, List<T> _) in _adjacencyList.GetKeyValues())
        {
            yield return vector;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}