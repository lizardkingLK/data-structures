namespace dataStructures.Core.NonLinear.Shared.State;

public record TreeNode<T> where T : IComparable<T>
{
    public TreeNode<T>? Left { get; set; }
    public T Value { get; set; }
    public TreeNode<T>? Right { get; set; }

    public TreeNode(TreeNode<T>? left, T value, TreeNode<T>? right)
    {
        Left = left;
        Value = value;
        Right = right;
    }

    public TreeNode(T value) => Value = value;
}