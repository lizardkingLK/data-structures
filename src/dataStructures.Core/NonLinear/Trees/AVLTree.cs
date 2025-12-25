using dataStructures.Core.NonLinear.Shared.State;
using dataStructures.Core.NonLinear.Trees.Helpers;

namespace dataStructures.Core.NonLinear.Trees;

public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
{
    public new TreeNode<T> Insert(T value)
    => Root = AVLTree<T>.Insert(Root, value);

    public new TreeNode<T>? Delete(T value)
    {
        throw new NotImplementedException();
    }

    private static TreeNode<T> Insert(TreeNode<T>? parent, T value)
    {
        if (parent == null)
        {
            return new(value);
        }

        if (CompareHelper<T>.FirstLowerThanOrEqualsSecond(value, parent.Value))
        {
            parent.Left = AVLTree<T>.Insert(parent.Left, value);
        }
        else if (CompareHelper<T>.FirstGreaterThanSecond(value, parent.Value))
        {
            parent.Right = AVLTree<T>.Insert(parent.Right, value);
        }

        parent.Height = 1 + Math.Max(
            Height(parent.Left),
            Height(parent.Right));

        int balance = GetBalance(parent);

        if (balance > 1
        && parent.Left != null
        && CompareHelper<T>.FirstLowerThanOrEqualsSecond(
            value,
            parent.Left.Value))
        {
            return AVLTree<T>.RightRotate(parent);
        }

        if (balance < -1
        && parent.Right != null
        && CompareHelper<T>.FirstGreaterThanSecond(
            value,
            parent.Right.Value))
        {
            return AVLTree<T>.LeftRotate(parent);
        }

        if (balance > 1
        && parent.Left != null
        && CompareHelper<T>.FirstGreaterThanSecond(
            value,
            parent.Left.Value))
        {
            parent.Left = AVLTree<T>.LeftRotate(parent.Left);
            return AVLTree<T>.RightRotate(parent);
        }

        if (balance < -1
        && parent.Right != null
        && CompareHelper<T>.FirstLowerThanOrEqualsSecond(
            value,
            parent.Right.Value))
        {
            parent.Right = AVLTree<T>.RightRotate(parent.Right);
            return AVLTree<T>.LeftRotate(parent);
        }

        return parent;
    }

    private static TreeNode<T> LeftRotate(TreeNode<T> yNode)
    {
        TreeNode<T> xNode = yNode.Right!;
        TreeNode<T>? leftOfX = xNode.Left;

        xNode.Left = yNode;
        yNode.Right = leftOfX;

        yNode.Height = 1 + Math.Max(
            Height(yNode.Left),
            Height(yNode.Right));
        xNode.Height = 1 + Math.Max(
            Height(xNode.Left),
            Height(xNode.Right));

        return xNode;
    }

    private static TreeNode<T> RightRotate(TreeNode<T> yNode)
    {
        TreeNode<T> xNode = yNode.Left!;
        TreeNode<T>? rightOfX = xNode.Right;

        xNode.Right = yNode;
        yNode.Left = rightOfX;

        yNode.Height = 1 + Math.Max(
            Height(yNode.Left),
            Height(yNode.Right));
        xNode.Height = 1 + Math.Max(
            Height(xNode.Left),
            Height(xNode.Right));

        return xNode;
    }

    private static int Height(TreeNode<T>? node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private static int GetBalance(TreeNode<T>? node)
    {
        if (node == null)
        {
            return 0;
        }

        return Height(node.Left) - Height(node.Right);
    }
}