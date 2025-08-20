using dataStructures.Core.NonLinear.HashMaps.Abstractions;
using dataStructures.Core.NonLinear.HashMaps.State;

namespace dataStructures.Core.NonLinear.HashMaps;

public class RobinHoodHashingHashMap<K, V>(float loadFactor) : IHashMap<K, V>
{
    private float _loadFactor = loadFactor;

    public int Capacity { get; }

    public int Size { get; }

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public void Add(K key, V value)
    {
        throw new NotImplementedException();
    }

    public V Get(K key)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        throw new NotImplementedException();
    }

    public V Remove(K key)
    {
        throw new NotImplementedException();
    }

    public bool TryAdd(K key, V value)
    {
        throw new NotImplementedException();
    }

    public bool TryGet(K key, out V? value)
    {
        throw new NotImplementedException();
    }

    public bool TryRemove(K key, out V? value)
    {
        throw new NotImplementedException();
    }

    public bool TryUpdate(K key, V value)
    {
        throw new NotImplementedException();
    }

    public void Update(K key, V value)
    {
        throw new NotImplementedException();
    }
}