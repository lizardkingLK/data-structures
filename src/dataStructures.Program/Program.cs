using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Program;

class Program
{
    public static void TestAVLTrees()
    {
        AVLTree<int> avl1 = new(10, 30, 20);

        /*
        Node root = null;

        // Constructing tree given in the above figure
        root = Insert(root, 9);
        root = Insert(root, 5);
        root = Insert(root, 10);
        root = Insert(root, 0);
        root = Insert(root, 6);
        root = Insert(root, 11);
        root = Insert(root, -1);
        root = Insert(root, 1);
        root = Insert(root, 2);

        Console.WriteLine("Preorder traversal of the constructed AVL tree is:");
        PreOrder(root);

        root = DeleteNode(root, 10);

        Console.WriteLine("\nPreorder traversal after deletion of 10:");
        PreOrder(root);

        // Preorder traversal of the constructed AVL tree is 
        // 9 1 0 -1 5 2 6 10 11 
        // Preorder traversal after deletion of 10 
        // 1 0 -1 9 5 2 6 11 
        */

        foreach (int value in AVLTree<int>.GetValuesPreOrder(avl1.Root))
        {
            Console.WriteLine(value);
        }
    }

    static void Main()
    {
        
    }
}
