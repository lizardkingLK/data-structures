namespace dataStructures.Core.Shared;

public record HashNode<K, V>
{
    public K Key { get; init; }

    public V Value { get; set; }

    public HashNode(K key, V value)
    {
        Key = key;
        Value = value;
    }

    public void Deconstruct(out K key, out V value)
    {
        key = Key;
        value = Value;
    }
}