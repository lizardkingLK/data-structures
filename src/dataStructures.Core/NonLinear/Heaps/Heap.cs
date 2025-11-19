using dataStructures.Core.NonLinear.Shared.Abstractions;
using dataStructures.Core.NonLinear.Shared.State;

namespace dataStructures.Core.NonLinear.Heaps;

public class Heap<T> : ITree<T> where T : IComparable<T>
{
    public TreeNode<T>? Delete(T value)
    {
        throw new NotImplementedException();
    }

    public TreeNode<T> Insert(T value)
    {
        throw new NotImplementedException();
    }

    public void Invert()
    {
        throw new NotImplementedException();
    }

    public TreeNode<T>? Search(T value, out TreeNode<T>? parent)
    {
        throw new NotImplementedException();
    }
}