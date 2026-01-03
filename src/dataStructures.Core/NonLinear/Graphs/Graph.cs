using System.Collections;
using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;
using dataStructures.Core.Linear.Arrays.DynamicArray;
using dataStructures.Core.Linear.Stacks;
using dataStructures.Core.Linear.Stacks.Enums;
using dataStructures.Core.NonLinear.HashMaps;

namespace dataStructures.Core.NonLinear.Graphs;

public class Graph<T> : IEnumerable<T>
{
    private readonly HashMap<T, List<T>> _adjacencyList = new();

    public void AddVector(T vector)
    {
        if (!_adjacencyList.TryGet(vector, out _))
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