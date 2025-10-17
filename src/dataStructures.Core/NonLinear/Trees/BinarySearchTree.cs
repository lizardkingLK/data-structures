using dataStructures.Core.Linear.Queues.Enums;
using dataStructures.Core.Linear.Stacks.Enums;
using dataStructures.Core.NonLinear.Shared.Abstractions;
using dataStructures.Core.NonLinear.Shared.State;
using dataStructures.Core.NonLinear.Trees.Enums;
using static dataStructures.Core.NonLinear.Trees.Enums.OrderTypeEnum;

namespace dataStructures.Core.NonLinear.Trees;

public class BinarySearchTree<T> : ITree<T> where T : IComparable<T>
{
    public TreeNode<T>? Root;

    public IEnumerable<T> ValuesPreOrder => GetValues(PreOrder);
    public IEnumerable<T> ValuesInOrder => GetValues(InOrder);
    public IEnumerable<T> ValuesPostOrder => GetValues(PostOrder);
    public IEnumerable<T> DepthFirstSearch => DFS(Root);
    public IEnumerable<T> BreadthFirstSearch => BFS(Root);

    public int Size = 0;

    public BinarySearchTree(params T[] values)
    {
        AddRange(values);
    }

    public BinarySearchTree(TreeNode<T> root)
    {
        Root = root;
    }

    public void AddRange(T[] values)
    {
        foreach (T value in values)
        {
            _ = Insert(value);
            Size++;
        }
    }

    public TreeNode<T> Insert(T value)
    {
        TreeNode<T> newNode = new(value);
        if (Root == null)
        {
            Root = newNode;
            return Root;
        }

        TreeNode<T>? current = Root;
        TreeNode<T>? parent = Root;
        int comparedResult;
        while (current != null)
        {
            comparedResult = value.CompareTo(current.Value);
            if (comparedResult <= 0)
            {
                current = current.Left;
            }
            else if (comparedResult > 0)
            {
                current = current.Right;
            }

            if (current != null || parent == null)
            {
                parent = current;
                continue;
            }

            comparedResult = value.CompareTo(parent.Value);
            if (comparedResult <= 0)
            {
                parent.Left = newNode;
            }
            else if (comparedResult > 0)
            {
                parent.Right = newNode;
            }

            break;
        }

        return newNode;
    }

    public TreeNode<T>? Search(T value, out TreeNode<T>? parent)
    {
        parent = default;

        TreeNode<T>? current = Root;
        int comparedResult;
        while (current != null)
        {
            comparedResult = value.CompareTo(current.Value);
            if (comparedResult == 0)
            {
                return current;
            }

            parent = current;
            if (comparedResult < 0)
            {
                current = current.Left;
            }
            else if (comparedResult > 0)
            {
                current = current.Right;
            }
        }

        return null;
    }

    public void Update(T oldValue, T newValue)
    {
        TreeNode<T>? current = Root;
        int comparedResult;
        while (current != null)
        {
            comparedResult = oldValue.CompareTo(current.Value);
            if (comparedResult == 0)
            {
                current.Value = newValue;
                return;
            }

            if (comparedResult < 0)
            {
                current = current.Left;
            }
            else if (comparedResult > 0)
            {
                current = current.Right;
            }
        }
    }

    public TreeNode<T>? Delete(T value)
    {
        TreeNode<T>? nodeToDelete = Search(value, out TreeNode<T>? parentNode)
        ?? throw new Exception("error. cannot find the given value");

        if (TryDeleteIfLeafNode(nodeToDelete, parentNode))
        {
            return nodeToDelete;
        }

        if (TryDeleteIfSingleNode(nodeToDelete, parentNode!))
        {
            return nodeToDelete;
        }

        if (TryDeleteIfMultipleNodes(nodeToDelete, parentNode))
        {
            return nodeToDelete;
        }

        return null;
    }

    public void Invert()
    {
        if (Root == null)
        {
            return;
        }

        Linear.Stacks.Stack<TreeNode<T>> items = new(StackTypeEnum.LinkedListTyped, Size);
        items.Push(Root);
        TreeNode<T>? current;
        TreeNode<T>? left;
        TreeNode<T>? right;
        while (!items.IsEmpty())
        {
            current = items.Pop();
            right = current.Right;
            left = current.Left;
            if (right != null)
            {
                items.Push(right);
            }

            if (left != null)
            {
                items.Push(left);
            }

            current.Left = right;
            current.Right = left;
        }
    }

    private bool TryDeleteIfMultipleNodes(TreeNode<T> nodeToDelete, TreeNode<T>? parentNode)
    {
        T? nextInOrderValue = GetValuesInOrder(nodeToDelete.Right).First();
        TreeNode<T> nextInOrder = Search(nextInOrderValue, out TreeNode<T>? parentOfNextInOrder)!;
        if (!TryDeleteIfLeafNode(nextInOrder, parentOfNextInOrder))
        {
            return false;
        }

        if (parentNode == null)
        {
            nextInOrder.Left = Root!.Left;
            nextInOrder.Right = Root!.Right;
            Root = nextInOrder;
            return true;
        }

        nextInOrder.Left = nodeToDelete.Left;
        nextInOrder.Right = nodeToDelete.Right;
        int comparedResult = nodeToDelete.Value.CompareTo(parentNode.Value);
        if (comparedResult <= 0)
        {
            parentNode.Left = nextInOrder;
        }
        else
        {
            parentNode.Right = nextInOrder;
        }

        return true;
    }

    private static bool TryDeleteIfSingleNode(TreeNode<T> nodeToDelete, TreeNode<T> parentNode)
    {
        if (nodeToDelete.Left != null && nodeToDelete.Right == null)
        {
            ReplaceChild(parentNode, nodeToDelete, nodeToDelete.Left);
            return true;
        }

        if (nodeToDelete.Right != null && nodeToDelete.Left == null)
        {
            ReplaceChild(parentNode, nodeToDelete, nodeToDelete.Right);
            return true;
        }

        return false;
    }

    private static void ReplaceChild(TreeNode<T> rootNode, TreeNode<T> parentNode, TreeNode<T> childNode)
    {
        int comparedResult = parentNode.Value.CompareTo(rootNode.Value);
        if (comparedResult <= 0)
        {
            rootNode.Left = childNode;
        }
        else
        {
            rootNode.Right = childNode;
        }
    }

    private bool TryDeleteIfLeafNode(TreeNode<T> nodeToDelete, TreeNode<T>? parentNode)
    {
        if (nodeToDelete.Left != null || nodeToDelete.Right != null)
        {
            return false;
        }

        if (parentNode == null)
        {
            Root = null;
            return true;
        }

        int comparedResult = nodeToDelete.Value.CompareTo(parentNode.Value);
        if (comparedResult <= 0)
        {
            parentNode.Left = null;
        }
        else
        {
            parentNode.Right = null;
        }

        return true;
    }

    private IEnumerable<T> GetValues(OrderTypeEnum orderTypeEnum)
    {
        return orderTypeEnum switch
        {
            PreOrder => BinarySearchTree<T>.GetValuesPreOrder(Root),
            InOrder => BinarySearchTree<T>.GetValuesInOrder(Root),
            PostOrder => BinarySearchTree<T>.GetValuesPostOrder(Root),
            _ => throw new NotImplementedException("error. order has not implemented yet"),
        };
    }

    // NLR
    private static IEnumerable<T> GetValuesPreOrder(TreeNode<T>? root)
    {
        if (root == null)
        {
            yield break;
        }

        yield return root.Value;

        foreach (T leftNode in GetValuesPreOrder(root.Left))
        {
            yield return leftNode;
        }

        foreach (T rightNode in GetValuesPreOrder(root.Right))
        {
            yield return rightNode;
        }
    }

    // LNR
    private static IEnumerable<T> GetValuesInOrder(TreeNode<T>? root)
    {
        if (root == null)
        {
            yield break;
        }

        foreach (T leftNode in GetValuesInOrder(root.Left))
        {
            yield return leftNode;
        }

        yield return root.Value;

        foreach (T rightNode in GetValuesInOrder(root.Right))
        {
            yield return rightNode;
        }
    }

    // LRN
    private static IEnumerable<T> GetValuesPostOrder(TreeNode<T>? root)
    {
        if (root == null)
        {
            yield break;
        }

        foreach (T leftNode in GetValuesPostOrder(root.Left))
        {
            yield return leftNode;
        }

        foreach (T rightNode in GetValuesPostOrder(root.Right))
        {
            yield return rightNode;
        }

        yield return root.Value;
    }

    private IEnumerable<T> DFS(TreeNode<T>? rootNode)
    {
        if (rootNode == null)
        {
            yield break;
        }

        Linear.Stacks.Stack<TreeNode<T>> items = new(StackTypeEnum.LinkedListTyped, Size);
        items.Push(rootNode);
        TreeNode<T>? current;
        while (!items.IsEmpty())
        {
            current = items.Pop();
            if (current.Right != null)
            {
                items.Push(current.Right);
            }

            if (current.Left != null)
            {
                items.Push(current.Left);
            }

            yield return current.Value;
        }
    }

    private IEnumerable<T> BFS(TreeNode<T>? rootNode)
    {
        if (rootNode == null)
        {
            yield break;
        }

        Linear.Queues.Queue<TreeNode<T>> items = new(QueueTypeEnum.LinkedListTyped, Size);
        items.Insert(rootNode);
        TreeNode<T>? current;
        while (!items.IsEmpty())
        {
            current = items.Remove()!;

            if (current.Left != null)
            {
                items.Insert(current.Left);
            }

            if (current.Right != null)
            {
                items.Insert(current.Right);
            }

            yield return current.Value;
        }
    }
}