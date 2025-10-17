using dataStructures.Core.NonLinear.Shared.Abstractions;

namespace dataStructures.Core.NonLinear.Heaps;

public class Heap<T> : ITree<T> where T : IComparable<T>
{
    public Shared.State.TreeNode<T>? Delete(T value)
    {
        throw new NotImplementedException();
    }

    public Shared.State.TreeNode<T> Insert(T value)
    {
        throw new NotImplementedException();
    }

    public void Invert()
    {
        throw new NotImplementedException();
    }

    public Shared.State.TreeNode<T>? Search(T value, out Shared.State.TreeNode<T>? parent)
    {
        throw new NotImplementedException();
    }
}