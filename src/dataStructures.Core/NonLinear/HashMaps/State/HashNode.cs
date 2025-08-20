namespace dataStructures.Core.NonLinear.HashMaps.State;

public record HashNode<K, V>(K Key, V Value, int PSL = 0)
{
    public K Key { get; init; } = Key;

    public V Value { get; set; } = Value;

    public int PSL { get; set; } = PSL;
}