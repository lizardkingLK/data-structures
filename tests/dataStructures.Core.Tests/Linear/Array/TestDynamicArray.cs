using dataStructures.Core.Linear.Array;

namespace dataStructures.Core.Tests.Linear.Array;

[Collection(nameof(TestDynamicArray))]
public class TestDynamicArray
{
    [Theory]
    [InlineData(-4)]
    [InlineData(0)]
    [InlineData(-7)]
    public void Should_Test_Instantiation(int capacity)
    {
        // Act
        DynamicArray<int>? dynamicArray = null;
        void InstantiateDynamicArray()
        {
            dynamicArray = new DynamicArray<int>(capacity);
        }

        // Assert
        Exception exception = Assert.Throws<Exception>(InstantiateDynamicArray);
        Assert.Equal("error. cannot create. invalid capacity", exception.Message);
        Assert.Null(dynamicArray);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(0)]
    public void Should_Test_Add(int value)
    {
        // Arrange
        DynamicArray<int> dynamicArray = new();

        // Act
        dynamicArray.Add(value);
        int addedValue = dynamicArray.Get(0);

        // Assert
        Assert.Equal(value, addedValue);
        Assert.Equal(1, dynamicArray.Size);
    }

    [Fact]
    public void Should_Test_Add_At_Index()
    {
        // Arrange
        DynamicArray<int> dynamicArray = new();

        // Act
        dynamicArray.Add(0, 1);
        dynamicArray.Add(0, 2);
        dynamicArray.Add(0, 3);
        dynamicArray.Add(0, 4);
        dynamicArray.Add(0, 5);

        // Assert
        int i;
        int[] values = [.. dynamicArray.Values];
        int length = 5;
        for (i = 0; i < length; i++)
        {
            Assert.Equal(length - i, values[i]);
        }
    }
}