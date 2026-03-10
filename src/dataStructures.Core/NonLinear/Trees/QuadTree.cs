namespace dataStructures.Core.NonLinear.Trees;

public class QuadTree<T>
{
    public struct Point(int y, int x)
    {
        public int Y { get; set; } = y;
        public int X { get; set; } = x;

        public static implicit operator Point((int y, int x) point)
        => new(point.y, point.x);
    }

    struct Node(Point point, T data)
    {
        public Point Point { get; set; } = point;
        public T Data { get; set; } = data;
    }

    private Point _topLeft;
    private Point _bottomRight;

    private Node _n;

    private QuadTree<T>? _topLeftTree;
    private QuadTree<T>? _topRightTree;
    private QuadTree<T>? _bottomLeftTree;
    private QuadTree<T>? _bottomRightTree;

    public QuadTree()
    {
        _topLeft = new(0, 0);
        _bottomRight = new(0, 0);

        _n = new();
    }

    public QuadTree(Point topLeft, Point bottomRight)
    {
        _topLeft = topLeft;
        _bottomRight = bottomRight;

        _n = new();
    }

    public void Insert(Node node)
    {
        if (node.Equals(default))
        {
            return;
        }

        if (!InBoundary(node.Point))
        {
            return;
        }

        if (Math.Abs(_topLeft.X - _bottomRight.X) <= 1
        && Math.Abs(_topLeft.Y - _bottomRight.Y) <= 1)
        {
            if (_n.Equals(default))
            {
                _n = node;
            }

            return;
        }

        if ((_topLeft.X + _bottomRight.X) / 2 >= node.Point.X)
        {
            if ((_topLeft.Y + _bottomRight.Y) / 2 >= node.Point.Y)
            {
                _topLeftTree ??= new(
                    new(_topLeft.Y, _topLeft.X),
                    new((_topLeft.Y + _bottomRight.Y) / 2,
                    (_topLeft.X + _bottomRight.X) / 2));
                _topLeftTree.Insert(node);
            }
            else
            {
                _bottomLeftTree ??= new(
                    new((_topLeft.Y + _bottomRight.Y) / 2,
                    _topLeft.X),
                    new(_bottomRight.Y,
                    (_topLeft.X + _bottomRight.X) / 2));
                _bottomLeftTree.Insert(node);
            }
        }
        else
        {
            if ((_topLeft.Y + _bottomRight.Y) / 2 >= node.Point.Y)
            {
                _topRightTree ??= new(
                    new(_topLeft.Y,
                    (_topLeft.X + _bottomRight.X) / 2),
                    new((_topLeft.Y + _bottomRight.Y) / 2,
                    _bottomRight.X));
                _topRightTree.Insert(node);
            }
            else
            {
                _bottomRightTree ??= new(
                    new((_topLeft.Y + _bottomRight.Y) / 2,
                    (_topLeft.X + _bottomRight.X) / 2),
                    new(_bottomRight.Y,
                    _bottomRight.X));
                _bottomRightTree.Insert(node);
            }
        }
    }

    private bool InBoundary(Point point)
    {
        return point.Y >= _topLeft.Y
        && point.Y <= _bottomRight.Y
        && point.X >= _topLeft.X
        && point.X <= _bottomRight.X;
    }
}