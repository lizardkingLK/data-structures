using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        BinarySearchTree<int> bst1 = new();
        bst1.AddRange([20, 10, 40, 30, 50, 60]);
        Console.WriteLine(string.Join(' ', bst1.BreadthFirstSearch));
        Console.WriteLine(string.Join(' ', bst1.DepthFirstSearch));
        bst1.Invert();
        Console.WriteLine(string.Join(' ', bst1.BreadthFirstSearch));
        /*
                20
            10      40
                  30  50
                        60
            */

        /*
                    20
                40      10
            50    30
        60                 
        */

    }
}
