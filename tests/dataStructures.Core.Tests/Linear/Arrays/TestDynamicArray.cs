using dataStructures.Core.Linear.Arrays;
using static dataStructures.Core.Linear.Arrays.Shared.Constants;

namespace dataStructures.Core.Tests.Linear.Arrays;

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
        Exception exception = Assert.Throws<ApplicationException>(InstantiateDynamicArray);
        Assert.Equal(ErrorInvalidCapacity, exception.Message);
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
        int addedValue = dynamicArray.GetValue(0);

        // Assert
        Assert.Equal(value, addedValue);
        Assert.Equal(1, dynamicArray.Size);
    }

    [Fact]
    public void Should_Test_Add_At_Index()
    {
        // Arrange
        DynamicArray<int> dynamicArray = new(5);

        // Act
        dynamicArray.Insert(0, 5);
        dynamicArray.Insert(1, 4);
        dynamicArray.Insert(2, 3);
        dynamicArray.Insert(3, 2);
        dynamicArray.Insert(4, 1);

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