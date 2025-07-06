namespace dataStructures.Core.Shared;

public struct HashNode<K, V>(K key, V value)
{
    public readonly K Key { get; } = key;

    public V Value { get; set; } = value;

    public override readonly string ToString()
    {
        return string.Format("Key = {0}, Value = {1}", Key, Value);
    }
}