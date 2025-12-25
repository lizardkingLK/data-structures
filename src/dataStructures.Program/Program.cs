using dataStructures.Core.Linear.Queues;
using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        AVLTree<int> avl1 = new(10, 30, 20);


        foreach (int value in AVLTree<int>.GetValuesPreOrder(avl1.Root))
        {
            Console.WriteLine(value);
        }
    }
}
