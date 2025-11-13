using dataStructures.Core.NonLinear.HashMaps;
using dataStructures.Core.NonLinear.HashMaps.State;
using static dataStructures.Core.NonLinear.HashMaps.Enums.HashTypeEnum;

namespace dataStructures.Core.Tests.NonLinear.HashMaps;

[Collection(nameof(TestSeparateChainingHashMap))]
public class TestSeparateChainingHashMap
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-4)]
    [InlineData(1)]
    [InlineData(2)]
    public void Should_Test_HashMap_Construction(float loadFactor)
    {
        // Arrange
        HashMap<int, string>? hashMap = null;

        void ConstructHashMap() => hashMap = new(ClosedAddressingSeparateChaining, loadFactor);

        // Act
        Exception exception = Assert.Throws<Exception>(ConstructHashMap);

        // Assert
        Assert.Equal("error. cannot create hashmap. invalid load factor argument", exception.Message);
        Assert.Null(hashMap);
    }

    [Fact]
    public void Should_Test_HashMap_Get_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        void GetItem() => hashMap.Get(5);

        // Act
        Exception exception = Assert.Throws<Exception>(GetItem);

        // Assert
        Assert.Equal("error. cannot get value. key does not contain", exception.Message);
    }

    [Fact]
    public void Should_Test_HashMap_TryGet_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        bool? couldGet = hashMap.TryGet(5, out string? value);

        // Assert
        Assert.False(couldGet);
        Assert.Null(value);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Get_Method_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        hashMap.Add(key, item);
        string value = hashMap.Get(key);

        // Assert
        Assert.Equal(item, value);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Get_Method_When_Contains_But_Incorrect_Key(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);
        void GetItem() => hashMap.Get(key + 1);

        // Act
        Exception exception = Assert.Throws<Exception>(GetItem);

        // Assert
        Assert.Equal("error. cannot get value. key does not contain", exception.Message);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryGet_Method_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        hashMap.Add(key, item);
        bool? couldGet = hashMap.TryGet(key, out string? value);

        // Assert
        Assert.True(couldGet);
        Assert.Equal(item, value);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryGet_Method_When_Contains_But_Incorrect_Key(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        hashMap.Add(key, item);
        bool? couldGet = hashMap.TryGet(key + 1, out string? value);

        // Assert
        Assert.False(couldGet);
        Assert.Null(value);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryAdd_Method(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        bool? couldAdd = hashMap.TryAdd(key, item);

        // Assert
        Assert.True(couldAdd);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Add_Method_When_Same_Inserted(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);
        void AddItem() => hashMap.Add(key, item);

        // Act
        Exception exception = Assert.Throws<Exception>(AddItem);

        // Assert
        Assert.Equal("error. cannot add value. key already contain", exception.Message);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryAdd_Method_When_Same_Inserted(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        bool? couldAdd = hashMap.TryAdd(key, item);

        // Assert
        Assert.False(couldAdd);
    }

    [Fact]
    public void Should_Test_HashMap_Add_Method_When_Items_To_Same_Bucket()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        hashMap.Add(10, "ten");
        string item1 = hashMap.Get(10);

        hashMap.Add(20, "twenty");
        string item2 = hashMap.Get(20);

        // Assert
        Assert.Equal("ten", item1);
        Assert.Equal("twenty", item2);
    }

    [Fact]
    public void Should_Test_HashMap_TryAdd_Method_When_Items_To_Same_Bucket()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        bool couldAdd1 = hashMap.TryAdd(10, "ten");
        string item1 = hashMap.Get(10);

        bool couldAdd2 = hashMap.TryAdd(20, "twenty");
        string item2 = hashMap.Get(20);

        // Assert
        Assert.True(couldAdd1);
        Assert.Equal("ten", item1);
        Assert.True(couldAdd2);
        Assert.Equal("twenty", item2);
    }

    [Fact]
    public void Should_Test_HashMap_Update_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        void UpdateItem() => hashMap.Update(5, "five");

        // Act
        Exception exception = Assert.Throws<Exception>(UpdateItem);

        // Assert
        Assert.Equal("error. cannot update value. key does not contain", exception.Message);
    }

    [Fact]
    public void Should_Test_HashMap_TryUpdate_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        bool? couldUpdate = hashMap.TryUpdate(5, "five");

        // Assert
        Assert.False(couldUpdate);
    }

    [Theory]
    [InlineData(-1, "minus_one", "Minus_One")]
    [InlineData(5, "five", "Five")]
    [InlineData(0, "zero", "Zero")]
    [InlineData(2, "two", "Two")]
    [InlineData(1, "one", "One")]
    public void Should_Test_HashMap_Update_Method_When_Contains(int key, string before, string after)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, before);
        string? gotBefore = hashMap.Get(key);

        // Act
        hashMap.Update(key, after);
        string? gotAfter = hashMap.Get(key);

        // Assert
        Assert.Equal(before, gotBefore);
        Assert.Equal(after, gotAfter);
    }

    [Theory]
    [InlineData(-1, "minus_one", "Minus_One")]
    [InlineData(5, "five", "Five")]
    [InlineData(0, "zero", "Zero")]
    [InlineData(2, "two", "Two")]
    [InlineData(1, "one", "One")]
    public void Should_Test_HashMap_TryUpdate_Method_When_Contains(int key, string before, string after)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, before);
        string? gotBefore = hashMap.Get(key);

        // Act
        bool? couldUpdate = hashMap.TryUpdate(key, after);
        string? gotAfter = hashMap.Get(key);

        // Assert
        Assert.True(couldUpdate);
        Assert.Equal(before, gotBefore);
        Assert.Equal(after, gotAfter);
    }

    [Theory]
    [InlineData(-1, "minus_one", "Minus_One")]
    [InlineData(5, "five", "Five")]
    [InlineData(0, "zero", "Zero")]
    [InlineData(2, "two", "Two")]
    [InlineData(1, "one", "One")]
    public void Should_Test_HashMap_Update_Method_When_Contains_But_Invalid_Key(int key, string before, string after)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, before);

        void UpdateItem() => hashMap.Update(key + 1, after);

        // Act
        Exception exception = Assert.Throws<Exception>(UpdateItem);

        // Assert
        Assert.Equal("error. cannot update value. key does not contain", exception.Message);
    }

    [Theory]
    [InlineData(-1, "minus_one", "Minus_One")]
    [InlineData(5, "five", "Five")]
    [InlineData(0, "zero", "Zero")]
    [InlineData(2, "two", "Two")]
    [InlineData(1, "one", "One")]
    public void Should_Test_HashMap_TryUpdate_Method_When_Contains_But_Invalid_Key(int key, string before, string after)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, before);

        // Act
        bool? couldUpdate = hashMap.TryUpdate(key + 1, after);

        // Assert
        Assert.False(couldUpdate);
    }

    [Fact]
    public void Should_Test_HashMap_Remove_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        string? removed = null;

        // Act
        void RemoveItem() => removed = hashMap.Remove(5);

        // Assert
        Exception exception = Assert.Throws<Exception>(RemoveItem);
        Assert.Equal("error. cannot remove value. key does not contain", exception.Message);
        Assert.Null(removed);
    }

    [Fact]
    public void Should_Test_HashMap_TryRemove_Method_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        bool? couldRemove = hashMap.TryRemove(5, out string? removed);

        // Assert
        Assert.False(couldRemove);
        Assert.Null(removed);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Remove_Method_When_Contain(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        string removed = hashMap.Remove(key);
        bool? couldGet = hashMap.TryGet(key, out string? gotAfter);

        // Assert
        Assert.Equal(item, removed);
        Assert.False(couldGet);
        Assert.Null(gotAfter);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryRemove_Method_When_Contain(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        bool? couldRemove = hashMap.TryRemove(key, out string? removed);
        bool? couldGet = hashMap.TryGet(key, out string? gotAfter);

        // Assert
        Assert.True(couldRemove);
        Assert.Equal(item, removed);
        Assert.False(couldGet);
        Assert.Null(gotAfter);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Remove_Method_When_Contain_But_Invalid_Key(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        string? removed = null;

        // Act
        void RemoveItem() => hashMap.Remove(key + 1);

        // Assert
        Exception exception = Assert.Throws<Exception>(RemoveItem);
        Assert.Equal("error. cannot remove value. key does not contain", exception.Message);
        Assert.Null(removed);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_TryRemove_Method_When_Contain_But_Invalid_Key(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        bool? couldRemove = hashMap.TryRemove(key + 1, out string? removed);

        // Assert
        Assert.False(couldRemove);
        Assert.Null(removed);
    }

    [Fact]
    public void Should_Test_HashMap_Add_Method_When_Remove_Then_ReAdd()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(10, "ten");

        string? removed = hashMap.Remove(10);

        // Act
        bool? couldReAdd = hashMap.TryAdd(10, "ten");

        // Assert
        Assert.Equal("ten", removed);
        Assert.True(couldReAdd);
    }

    [Fact]
    public void Should_Test_HashMap_Add_Method_When_TryRemove_Then_ReAdd()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(10, "ten");

        bool? couldRemove = hashMap.TryRemove(10, out string? removed);

        // Act
        bool? couldReAdd = hashMap.TryAdd(10, "ten");

        // Assert
        Assert.True(couldRemove);
        Assert.Equal("ten", removed);
        Assert.True(couldReAdd);
    }

    [Fact]
    public void Should_Test_HashMap_GetHashNodes_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        IEnumerable<HashNode<int, string>> hashNodes = hashMap.GetHashNodes();

        // Assert
        Assert.Empty(hashNodes);
    }

    [Fact]
    public void Should_Test_HashMap_GetKeyValues_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        IEnumerable<KeyValuePair<int, string>> keyValues = hashMap.GetKeyValues();

        // Assert
        Assert.Empty(keyValues);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_GetHashNodes_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        IEnumerable<HashNode<int, string>> hashNodes = hashMap.GetHashNodes();

        // Assert
        Assert.Contains(new(key, item), hashNodes);
    }

    [Fact]
    public void Should_Test_HashMap_GetHashNodes_When_Contains_Many()
    {
        HashMap<int, string> hashMap = new(ClosedAddressingSeparateChaining);

        hashMap.Add(27, "27");
        hashMap.Add(43, "43");
        hashMap.Add(692, "692");
        hashMap.Add(72, "72");

        // Act
        IEnumerable<HashNode<int, string>> hashNodes = hashMap.GetHashNodes();

        // Assert
        Assert.Contains(new(27, "27"), hashNodes);
        Assert.Contains(new(43, "43"), hashNodes);
        Assert.Contains(new(692, "692"), hashNodes);
        Assert.Contains(new(72, "72"), hashNodes);
    }

    [Fact]
    public void Should_Test_HashMap_GetHashNodes_When_Contains_Many_Quadratic()
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(4, "4");
        hashMap.Add(8, "8");
        hashMap.Add(12, "12");
        hashMap.Add(16, "16");
        hashMap.Add(20, "20");
        hashMap.Add(24, "24");
        hashMap.Add(28, "28");
        hashMap.Add(32, "32");

        // Act
        IEnumerable<HashNode<int, string>> hashNodes = hashMap.GetHashNodes();

        // Assert
        Assert.Contains(new(4, "4"), hashNodes);
        Assert.Contains(new(8, "8"), hashNodes);
        Assert.Contains(new(12, "12"), hashNodes);
        Assert.Contains(new(16, "16"), hashNodes);
        Assert.Contains(new(20, "20"), hashNodes);
        Assert.Contains(new(24, "24"), hashNodes);
        Assert.Contains(new(28, "28"), hashNodes);
        Assert.Contains(new(32, "32"), hashNodes);
    }

    [Fact]
    public void Should_Test_HashMap_GetHashNodes_When_Contains_Many_Mixed_Keys()
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(4, "4");
        hashMap.Add(27, "27");
        hashMap.Add(8, "8");
        hashMap.Add(43, "43");
        hashMap.Add(12, "12");
        hashMap.Add(692, "692");
        hashMap.Add(16, "16");
        hashMap.Add(72, "72");
        hashMap.Add(20, "20");
        hashMap.Add(24, "24");
        hashMap.Add(28, "28");
        hashMap.Add(32, "32");

        // Act
        IEnumerable<HashNode<int, string>> hashNodes = hashMap.GetHashNodes();

        // Assert
        Assert.Contains(new(4, "4"), hashNodes);
        Assert.Contains(new(27, "27"), hashNodes);
        Assert.Contains(new(8, "8"), hashNodes);
        Assert.Contains(new(43, "43"), hashNodes);
        Assert.Contains(new(12, "12"), hashNodes);
        Assert.Contains(new(692, "692"), hashNodes);
        Assert.Contains(new(16, "16"), hashNodes);
        Assert.Contains(new(72, "72"), hashNodes);
        Assert.Contains(new(20, "20"), hashNodes);
        Assert.Contains(new(24, "24"), hashNodes);
        Assert.Contains(new(28, "28"), hashNodes);
        Assert.Contains(new(32, "32"), hashNodes);
    }

    [Fact]
    public void Should_Test_HashMap_GetHashNodes_When_Contains_Many_Random_Keys()
    {
        // Arrange
        HashMap<int, int> hashMap = new();

        List<HashNode<int, int>> itemsList = [.. Enumerable
        .Range(0, 200)
        .Select((_, index) => Random.Shared.Next(-20, 20))
        .Distinct()
        .Select(item => new HashNode<int, int>(item, item))];

        foreach ((int key, int value, _, _) in itemsList)
        {
            hashMap.Add(key, value);
        }

        // Act
        IEnumerable<HashNode<int, int>> hashNodes = hashMap.GetHashNodes();

        // Assert
        foreach ((int key, int value, _, _) in hashNodes)
        {
            Assert.Contains(new(key, value), itemsList);
        }
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_GetKeyValues_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        IEnumerable<KeyValuePair<int, string>> keyValues = hashMap.GetKeyValues();

        // Assert
        Assert.Contains(new(key, item), keyValues);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_GetHashNodes_When_Contains_And_Removed(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        List<HashNode<int, string>> hashNodesBefore = [.. hashMap
        .GetHashNodes()
        .Select(hashNode => new HashNode<int, string>(key, hashNode.Value))];

        // Act
        hashMap.Remove(key);
        List<HashNode<int, string>> hashNodesAfter = [.. hashMap.GetHashNodes()];

        // Assert
        Assert.Contains(new(key, item), hashNodesBefore);
        Assert.Empty(hashNodesAfter);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_GetKeyValues_When_Contains_And_Removed(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        List<KeyValuePair<int, string>> keyValuesBefore = [.. hashMap.GetKeyValues()];

        // Act
        hashMap.Remove(key);
        List<KeyValuePair<int, string>> keyValuesAfter = [.. hashMap.GetKeyValues()];

        // Assert
        Assert.Contains(new KeyValuePair<int, string>(key, item), keyValuesBefore);
        Assert.Empty(keyValuesAfter);
    }

    [Fact]
    public void Should_Test_HashMap_Capacity_Property_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        int capacity = hashMap.Capacity;

        // Assert
        Assert.Equal(Core.NonLinear.HashMaps.Shared.Constants.INITIAL_CAPACITY, capacity);
    }

    [Fact]
    public void Should_Test_HashMap_Size_Property_When_Empty()
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        // Act
        int size = hashMap.Size;

        // Assert
        Assert.Equal(0, size);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Capacity_Property_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        int capacity = hashMap.Capacity;

        // Assert
        Assert.Equal(2, capacity);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_HashMap_Size_Property_When_Contains(int key, string item)
    {
        // Arrange
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        int size = hashMap.Size;

        // Assert
        Assert.Equal(1, size);
    }

    [Fact]
    public void Should_Test_HashMap_Capacity_Property_When_Doubled()
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(-1, "minus_one");
        hashMap.Add(1, "one");

        // Act
        int capacity = hashMap.Capacity;

        // Assert
        Assert.Equal(4, capacity);
    }

    [Fact]
    public void Should_Test_HashMap_Size_Property_When_Doubled()
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(-1, "minus_one");
        hashMap.Add(1, "one");

        // Act
        int size = hashMap.Size;

        // Assert
        Assert.Equal(2, size);
    }

    [Fact]
    public void Should_Test_HashMap_Capacity_Property_When_Added_Many()
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(27, "27");
        hashMap.Add(43, "43");
        hashMap.Add(692, "692");
        hashMap.Add(72, "72");

        // Act
        int capacity = hashMap.Capacity;

        // Assert
        Assert.Equal(8, capacity);
    }

    [Theory]
    [InlineData(-1, "minus_one")]
    [InlineData(5, "five")]
    [InlineData(0, "zero")]
    [InlineData(2, "two")]
    [InlineData(1, "one")]
    public void Should_Test_Int_Indexer_Accessor(int key, string item)
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, item);

        // Act
        string value = hashMap[key];

        // Assert
        Assert.Equal(value, item);
    }

    [Theory]
    [InlineData(-1, "minus_one", "MinusOne")]
    [InlineData(5, "five", "Five")]
    [InlineData(0, "zero", "Zero")]
    [InlineData(2, "two", "Two")]
    [InlineData(1, "one", "One")]
    public void Should_Test_Int_Indexer_Mutator(int key, string before, string after)
    {
        HashMap<int, string> hashMap = new();

        hashMap.Add(key, before);
        string gotBefore = hashMap.Get(key);

        // Act
        hashMap[key] = after;
        string gotAfter = hashMap.Get(key);

        // Assert
        Assert.Equal(before, gotBefore);
        Assert.Equal(after, gotAfter);
    }
}