namespace dataStructures.Core.NonLinear.HashMaps.State;

public record HashNode<K, V>(K Key, V Value, int? PSL = null)
{
    public K Key { get; init; } = Key;

    public V Value { get; set; } = Value;

    public int? PSL { get; set; } = PSL;
}