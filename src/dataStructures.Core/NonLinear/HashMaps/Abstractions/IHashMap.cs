using dataStructures.Core.NonLinear.HashMaps.State;

namespace dataStructures.Core.NonLinear.HashMaps.Abstractions;

public interface IHashMap<K, V>
{
    V this[K key] { get; set; }
    public int Capacity { get; }
    public int Size { get; }
    void Add(K key, V value);
    bool TryAdd(K key, V value);
    V Get(K key);
    bool TryGet(K key, out V? value);
    void Update(K key, V value);
    bool TryUpdate(K key, V value);
    V Remove(K key);
    bool TryRemove(K key, out V? value);
    IEnumerable<HashNode<K, V>> GetHashNodes();
    IEnumerable<KeyValuePair<K, V>> GetKeyValues();
}