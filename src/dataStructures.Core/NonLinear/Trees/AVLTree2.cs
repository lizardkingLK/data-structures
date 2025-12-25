namespace dataStructures.Core.NonLinear.Trees;

#nullable disable
public class Node(int key)
{
    public int Key = key;
    public Node Left = null;
    public Node Right = null;
    public int Height = 1;
}

public class AVLTree2
{
    // A utility function to get 
    // the height of the tree
    static int Height(Node node)
    {
        if (node == null)
            return 0;

        return node.Height;
    }

    // A utility function to right rotate
    // subtree rooted with y
    static Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        // Perform rotation
        x.Right = y;
        y.Left = T2;

        // Update heights
        y.Height = 1 + Math.Max(Height(y.Left),
                                Height(y.Right));
        x.Height = 1 + Math.Max(Height(x.Left),
                                Height(x.Right));

        // Return new root
        return x;
    }

    // A utility function to left rotate 
    // subtree rooted with x
    static Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        // Perform rotation
        y.Left = x;
        x.Right = T2;

        // Update heights
        x.Height = 1 + Math.Max(Height(x.Left),
                                Height(x.Right));
        y.Height = 1 + Math.Max(Height(y.Left),
                                Height(y.Right));

        // Return new root
        return y;
    }

    // Get balance factor of node N
    static int GetBalance(Node node)
    {
        if (node == null)
            return 0;
        return Height(node.Left) - Height(node.Right);
    }

    // Recursive function to insert a key in the 
    // subtree rooted with node
    public static Node Insert(Node node, int key)
    {

        // Perform the normal BST insertion
        if (node == null)
            return new Node(key);

        if (key < node.Key)
            node.Left = Insert(node.Left, key);
        else if (key > node.Key)
            node.Right = Insert(node.Right, key);
        else // Equal keys are not allowed in BST
            return node;

        // Update height of this ancestor node
        node.Height = 1 + Math.Max(Height(node.Left),
                                   Height(node.Right));

        // Get the balance factor of this ancestor node
        int balance = GetBalance(node);

        // If this node becomes unbalanced, 
        // then there are 4 cases

        // Left Left Case
        if (balance > 1 && key < node.Left.Key)
            return RightRotate(node);

        // Right Right Case
        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        // Left Right Case
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // Right Left Case
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        // Return the (unchanged) node pointer
        return node;
    }

    // A utility function to print preorder 
    // traversal of the tree
    public static void PreOrder(Node root)
    {
        if (root != null)
        {
            Console.Write(root.Key + " ");
            PreOrder(root.Left);
            PreOrder(root.Right);
        }
    }
}
#nullable enable