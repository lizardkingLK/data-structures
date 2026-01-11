namespace dataStructures.Core.NonLinear.Shared.State;

public record TreeNode<T> where T : IComparable<T>
{
    public TreeNode<T>? Left { get; set; }
    public T Value { get; set; }
    public TreeNode<T>? Right { get; set; }
    public int Height { get; set; } = 1;

    public TreeNode(TreeNode<T>? left, T value, TreeNode<T>? right)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    public TreeNode(T value) => Value = value;
}