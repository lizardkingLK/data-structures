using dataStructures.Core.Linear.Queues;
using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Program;

class Program
{
    static void TestAVLTree2()
    {
        Node? root = null;

        // Constructing tree given in the above figure
        root = AVLTree2.Insert(root, 10);
        root = AVLTree2.Insert(root, 20);
        root = AVLTree2.Insert(root, 30);
        root = AVLTree2.Insert(root, 40);
        root = AVLTree2.Insert(root, 50);
        root = AVLTree2.Insert(root, 25);

        /* The constructed AVL Tree would be
                  30 
                /   \ 
              20     40 
             /  \      \ 
           10   25     50 
        */

        // Preorder traversal
        AVLTree2.PreOrder(root);
    }

    static void Main()
    {
        var avl1 = new AVLTree<int>();
        avl1.Insert(50);
        avl1.Insert(70);
        avl1.Insert(30);
        avl1.Insert(20);
        avl1.Insert(10); // RR

        avl1.Insert(40); // LR

        foreach (int value in AVLTree<int>.GetValuesPreOrder(avl1.Root))
        {
            Console.WriteLine(value);
        }
    }
}
