namespace dataStructures.Core.NonLinear.Graphs;

public class UnionFind
{
    private readonly int[] _parents;
    private readonly int[] _rank;
    private readonly int _n;

    public UnionFind(int n)
    {
        _n = n;
        _rank = new int[n];
        _parents = new int[n];
        for (int i = 0; i < n; i++)
        {
            _parents[i] = i;
        }
    }

    public int Find(int x)
    {
        if (x < 0 || x >= _n)
        {
            return -1;
        }

        int xAt = _parents[x];
        if (xAt != x)
        {
            xAt = _parents[x] = Find(xAt);
        }

        return xAt;
    }

    public bool Union(int x, int y)
    {
        (int xAt, int yAt) = (Find(x), Find(y));
        if (xAt == yAt)
        {
            return false;
        }

        if (_rank[xAt] < _rank[yAt])
        {
            _parents[xAt] = yAt;
        }
        else if (_rank[xAt] > _rank[yAt])
        {
            _parents[yAt] = xAt;
        }
        else
        {
            _parents[xAt] = yAt;
            _rank[xAt]++;
        }

        return true;
    }
}