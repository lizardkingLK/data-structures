namespace dataStructures.Core.NonLinear.Trees.SegmentTree;

public class SegmentTreeLazy
{
    private const int MAX = (int)1e6;
    private readonly int[] _tree;
    private readonly int[] _lazy;

    public SegmentTreeLazy()
    {
        _tree = new int[MAX];
        _lazy = new int[MAX];
    }

    public void RangedUpdate(int si, int ss, int se, int us, int ue, int diff)
    {
        if (_lazy[si] != default)
        {
            _tree[si] += (se - ss + 1) * _lazy[si];
            if (ss != se)
            {
                _lazy[si * 2 + 1] += _lazy[si];
                _lazy[si * 2 + 2] += _lazy[si];
            }

            _lazy[si] = 0;
        }

        if (ss > se || ss > ue || se < us)
        {
            return;
        }

        if (ss >= us && se <= ue)
        {
            _tree[si] += (se - ss + 1) * diff;
            if (ss != se)
            {
                _lazy[si * 2 + 1] += diff;
                _lazy[si * 2 + 2] += diff;
            }

            return;
        }

        int mid = (ss + se) / 2;
        RangedUpdate(si * 2 + 1, ss, mid, us, ue, diff);
        RangedUpdate(si * 2 + 2, mid + 1, se, us, ue, diff);

        _tree[si] = _tree[si * 2 + 1] + _tree[si * 2 + 2];
    }

    public void UpdateRange(int node, int us, int ue, int diff)
    {
        RangedUpdate(0, 0, node - 1, us, ue, diff);
    }

    public int SumGet(int ss, int se, int qs, int qe, int si)
    {
        if (_lazy[si] != 0)
        {
            _tree[si] += (se - ss + 1) * _lazy[si];
            if (ss != se)
            {
                _lazy[si * 2 + 1] += _lazy[si];
                _lazy[si * 2 + 2] += _lazy[si];
            }

            _lazy[si] = 0;
        }

        if (ss > se || ss > qe || se < qs)
        {
            return 0;
        }

        if (ss >= qs && se <= qe)
        {
            return _tree[si];
        }

        int mid = (ss + se) / 2;

        return SumGet(ss, mid, qs, qe, 2 * si + 1)
        + SumGet(mid + 1, se, qs, qe, 2 * si + 2);
    }

    public int GetSum(int n, int qs, int qe)
    {
        if (qs < 0 || qe > n - 1 || qs > qe)
        {
            Console.WriteLine("Invalid Input");
            return -1;
        }

        return SumGet(0, n - 1, qs, qe, 0);
    }

    public void STConstruct(int[] arr, int ss, int se, int si)
    {
        if (ss > se)
        {
            return;
        }

        if (ss == se)
        {
            _tree[si] = arr[ss];
            return;
        }

        int mid = (ss + se) / 2;
        STConstruct(arr, ss, mid, si * 2 + 1);
        STConstruct(arr, mid + 1, se, si * 2 + 2);

        _tree[si] = _tree[si * 2 + 1] + _tree[si * 2 + 2];
    }

    public void ConstructST(int[] arr, int n)
    {
        STConstruct(arr, 0, n - 1, 0);
    }
}