using dataStructures.Core.NonLinear.Shared.State;
using dataStructures.Core.NonLinear.Trees;

namespace dataStructures.Core.Tests.NonLinear.Trees;

[Collection(nameof(TestBinarySearchTree))]
public class TestBinarySearchTree
{
    [Theory]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Insert_To_Binary_Search_Tree(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new();

        // Act
        foreach (int num in nums)
        {
            bst.Insert(num);
        }

        // Assert
        TreeNode<int>? root = bst.Root;
        Assert.NotNull(root);
        Assert.Equal(nums[0], root.Value);
        Assert.NotNull(root.Left);
        Assert.Equal(nums[1], root.Left.Value);
        Assert.NotNull(root.Right);
        Assert.Equal(nums[2], root.Right.Value);
    }

    [Theory]
    [InlineData(128, -1, 16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(90, -23, 12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(60, 100, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(1, 23, 2, 1, 3)]
    [InlineData(5, 3, 5, 4, 6)]
    [InlineData(7, 5, 8, 7, 9)]
    public void Should_Test_Search_Binary_Search_Tree(int existing, int nonExisting, params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        TreeNode<int>? possibleExisting = bst.Search(existing, out _);
        TreeNode<int>? impossibleExisting = bst.Search(nonExisting, out _);

        // Assert
        Assert.NotNull(possibleExisting);
        Assert.Equal(existing, possibleExisting.Value);
        Assert.Null(impossibleExisting);
    }

    [Theory]
    [InlineData(15, 15)]
    [InlineData(40, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(60, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(30, 50, 30, 20, 70, 60, 80)]
    [InlineData(30, 50, 30, 40, 70, 60, 80)]
    [InlineData(70, 50, 30, 20, 40, 70, 60)]
    [InlineData(70, 50, 30, 20, 40, 70, 80)]
    [InlineData(30, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(70, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(50, 50, 30, 20, 40, 70, 60, 80)]
    public void Should_Test_Delete_Binary_Search_Tree(int value, params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        TreeNode<int>? deletedNode = bst.Delete(value);
        TreeNode<int>? impossibleExisting = bst.Search(value, out _);

        // Assert
        Assert.NotNull(deletedNode);
        Assert.Equal(value, deletedNode.Value);
        Assert.Null(impossibleExisting);
    }

    [Theory]
    [InlineData(-1, 15)]
    [InlineData(-1, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(0, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(1, 50, 30, 20, 70, 60, 80)]
    [InlineData(1, 50, 30, 40, 70, 60, 80)]
    [InlineData(1, 50, 30, 20, 40, 70, 60)]
    [InlineData(1, 50, 30, 20, 40, 70, 80)]
    [InlineData(1, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 50, 30, 20, 40, 70, 60, 80)]
    [InlineData(3, 50, 30, 20, 40, 70, 60, 80)]
    public void Should_Test_Delete_Non_Existing_Value_From_Binary_Search_Tree(int value, params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        TreeNode<int>? deletedNode = null;
        void DeleteAction()
        {
            deletedNode = bst.Delete(value);
        }

        // Act
        TreeNode<int>? impossibleExisting = bst.Search(value, out _);

        // Assert
        Assert.Null(impossibleExisting);
        Exception? exception = Assert.Throws<Exception>(DeleteAction);
        Assert.Equal("error. cannot find the given value", exception.Message);
        Assert.Null(deletedNode);
    }

    [Theory]
    [InlineData(16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Values_Of_Binary_Search_Tree_PreOrder(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        int[] itemsInOrder = [.. bst.ValuesPreOrder];

        // Assert
        foreach (int item in itemsInOrder)
        {
            Assert.Contains(item, nums);
        }
    }

    [Theory]
    [InlineData(16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Values_Of_Binary_Search_Tree_InOrder(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        int[] itemsInOrder = [.. bst.ValuesInOrder];

        // Assert
        foreach (int item in itemsInOrder)
        {
            Assert.Contains(item, nums);
        }
    }

    [Theory]
    [InlineData(16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Values_Of_Binary_Search_Tree_PostOrder(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        int[] itemsPostOrder = [.. bst.ValuesPostOrder];

        // Assert
        foreach (int item in itemsPostOrder)
        {
            Assert.Contains(item, nums);
        }
    }

    [Theory]
    [InlineData(16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Values_Of_Binary_Search_Tree_DFS(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        int[] depthFirstSearchedItems = [.. bst.DepthFirstSearch];

        // Assert
        foreach (int item in depthFirstSearchedItems)
        {
            Assert.Contains(item, nums);
        }
    }

    [Theory]
    [InlineData(16, 2, 4, 8, 2, 2, 32, 64, 128, 32, 31, 33, 3)]
    [InlineData(12, 23, 34, 45, 56, 67, 78, 89, 90)]
    [InlineData(50, 30, 20, 40, 70, 60, 80)]
    [InlineData(2, 1, 3)]
    [InlineData(5, 4, 6)]
    [InlineData(8, 7, 9)]
    public void Should_Test_Values_Of_Binary_Search_Tree_BFS(params int[] nums)
    {
        // Arrange
        BinarySearchTree<int> bst = new(nums);

        // Act
        int[] breadthFirstSearchedItems = [.. bst.BreadthFirstSearch];

        // Assert
        foreach (int item in breadthFirstSearchedItems)
        {
            Assert.Contains(item, nums);
        }
    }
}