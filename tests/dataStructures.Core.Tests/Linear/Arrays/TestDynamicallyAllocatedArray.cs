using dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray;
using static dataStructures.Core.Linear.Arrays.DynamicallyAllocatedArray.Shared.Constants;

namespace dataStructures.Core.Tests.Linear.Arrays;

[Collection(nameof(TestDynamicallyAllocatedArray))]
public class TestDynamicallyAllocatedArray
{
    [Theory]
    [InlineData(-4)]
    [InlineData(0)]
    [InlineData(-7)]
    public void Should_Test_Instantiation(int capacity)
    {
        // Act
        DynamicallyAllocatedArray<int>? DynamicallyAllocatedArray = null;
        void InstantiateDynamicallyAllocatedArray()
        {
            DynamicallyAllocatedArray = new DynamicallyAllocatedArray<int>(capacity);
        }

        // Assert
        Exception exception = Assert.Throws<ApplicationException>(InstantiateDynamicallyAllocatedArray);
        Assert.Equal(ErrorInvalidCapacity, exception.Message);
        Assert.Null(DynamicallyAllocatedArray);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(0)]
    public void Should_Test_Add(int value)
    {
        // Arrange
        DynamicallyAllocatedArray<int> DynamicallyAllocatedArray = new();

        // Act
        DynamicallyAllocatedArray.Add(value);
        int addedValue = DynamicallyAllocatedArray.GetValue(0);

        // Assert
        Assert.Equal(value, addedValue);
        Assert.Equal(1, DynamicallyAllocatedArray.Size);
    }

    [Fact]
    public void Should_Test_Add_At_Index()
    {
        // Arrange
        DynamicallyAllocatedArray<int> DynamicallyAllocatedArray = new(5);

        // Act
        DynamicallyAllocatedArray.Insert(0, 5);
        DynamicallyAllocatedArray.Insert(1, 4);
        DynamicallyAllocatedArray.Insert(2, 3);
        DynamicallyAllocatedArray.Insert(3, 2);
        DynamicallyAllocatedArray.Insert(4, 1);

        // Assert
        int i;
        int[] values = [.. DynamicallyAllocatedArray.Values];
        int length = 5;
        for (i = 0; i < length; i++)
        {
            Assert.Equal(length - i, values[i]);
        }
    }
}