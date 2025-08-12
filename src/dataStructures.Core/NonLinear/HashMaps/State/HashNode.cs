namespace dataStructures.Core.NonLinear.HashMaps.State;

public record HashNode<K, V>(K Key, V Value)
{
    public K Key { get; init; } = Key;

    public V Value { get; set; } = Value;
}