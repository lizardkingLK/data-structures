using dataStructures.Core.NonLinear.Shared.State;

namespace dataStructures.Core.NonLinear.Shared.Abstractions;

public interface ITree<T> where T : IComparable<T>
{
    TreeNode<T> Insert(T value);
    TreeNode<T>? Search(T value, out TreeNode<T>? parent);
    TreeNode<T>? Delete(T value);
    void Invert();
}