using System.Numerics;

namespace dataStructures.Core.NonLinear.Trees;

public class SegmentTree<T> where T : INumber<T>
{
   
    private readonly int _size;
    private readonly T[] _array;
    private readonly T[] _values;

    public SegmentTree(T[] values)
    {
        _size = values.Length;
        _array = values;
        _values = new T[4 * _size];
    }

    public void Build(int node, int l, int r)
    {
        if (l == r)
        {
            _values[node] = _array[l];
        }
        else
        {
            int mid = (l + r) / 2;
            Build(2 * node, l, mid);
            Build(2 * node + 1, mid + 1, r);
            _values[node] = _values[2 * node] + _values[2 * node + 1];
        }
    }

    public void Update(int node, int l, int r, int index, T value)
    {
        if (l == r)
        {
            _array[index] += value;
            _values[node] += value;
        }
        else
        {
            int mid = (l + r) / 2;
            if (l <= index && index <= mid)
            {
                Update(2 * node, l, mid, index, value);
            }
            else
            {
                Update(2 * node + 1, mid + 1, r, index, value);
            }

            _values[node] = _values[2 * node] + _values[2 * node + 1];
        }
    }

    public T? Query(int node, int tl, int tr, int l, int r)
    {
        if (r < tl || tr < l)
        {
            return default;
        }

        if (l <= tl && tr <= r)
        {
            return _values[node];
        }

        int tm = (tl + tr) / 2;

        T? left = Query(2 * node, tl, tm, l, r);
        T? right = Query(2 * node + 1, tm + 1, tr, l, r);
        if (left == null)
        {
            return right;
        }

        if (right == null)
        {
            return left;
        }

        return left + right;
    }
}