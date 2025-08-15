using dataStructures.Core.NonLinear.HashMaps;
using static dataStructures.Core.NonLinear.HashMaps.Enums.HashTypeEnum;

namespace dataStructures.Core.Tests.NonLinear.HashMaps;

[Collection(nameof(TestClosedAddressingSeparateChainingHashMap))]
public class TestOpenAddressingHashMap
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-4)]
    public void Should_Test_HashMap_Construction(float loadFactor)
    {
        // Arrange
        HashMap<int, string>? hashMap = null;

        void ConstructHashMap() => hashMap = new(OpenAddressingLinearProbing, loadFactor);

        // Act
        Exception exception = Assert.Throws<Exception>(ConstructHashMap);

        // Assert
        Assert.Equal("error. cannot create hashmap. invalid load factor argument", exception.Message);
        Assert.Null(hashMap);
    }
}