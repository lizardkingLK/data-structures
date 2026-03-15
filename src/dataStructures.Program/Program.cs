using dataStructures.Core.NonLinear.Trees;
using dataStructures.Core.NonLinear.Trees.SegmentTree;

namespace dataStructures.Program;

class Program
{
    static void TestSegmentTrees()
    {
        var values = new int[] { 0, 1, 3, 5, -2, 3 };
        var st = new SegmentTree<int>(values);
        var n = values.Length;
        st.Build(1, 0, n - 1);
        Console.WriteLine(
            "Sum of values in range 0-4 are: {0}",
            st.Query(1, 0, n - 1, 0, 4));

        st.Update(1, 0, n - 1, 1, 100);
        Console.WriteLine(
            "Value at index 1 increased by 100");
        Console.WriteLine("sum of value in range 1-3 are: "
                          + st.Query(1, 0, n - 1, 1, 3));
    }

    static void TestSegmentTreesLazy()
    {
        int[] arr = [1, 3, 5, 7, 9, 11];
        int n = arr.Length;

        var st = new SegmentTreeLazy();
        st.ConstructST(arr, n);

        Console.WriteLine("Sum of values in given range = "
                          + st.GetSum(n, 1, 3));

        st.UpdateRange(n, 0, n - 1, 10);

        Console.WriteLine(
            "Updated sum of values in given range = "
            + st.GetSum(n, 0, n - 1));
    }

    static void Main()
    {
        // TestSegmentTrees();
        TestSegmentTreesLazy();
    }
}
