using dataStructures.Core.NonLinear.HashMaps.Abstractions;
using dataStructures.Core.NonLinear.HashMaps.Enums;
using dataStructures.Core.NonLinear.HashMaps.State;
using static dataStructures.Core.NonLinear.HashMaps.Enums.HashTypeEnum;
using static dataStructures.Core.NonLinear.HashMaps.Shared.Constants;

namespace dataStructures.Core.NonLinear.HashMaps;

public class HashMap<K, V> : IHashMap<K, V>
{
    private readonly IHashMap<K, V> _hashMap;

    public int Capacity { get => _hashMap.Capacity; }

    public int Size { get => _hashMap.Size; }

    public HashMap() : this(ClosedAddressingSeparateChaining, LOAD_FACTOR)
    {
    }

    public HashMap(float loadFactor) : this(ClosedAddressingSeparateChaining, loadFactor)
    {
    }

    public V this[K key]
    {
        get => _hashMap[key];
        set => _hashMap[key] = value;
    }

    public HashMap(HashTypeEnum hashType = ClosedAddressingSeparateChaining, float loadFactor = LOAD_FACTOR)
    {
        if (loadFactor <= 0)
        {
            throw new Exception("error. cannot create hashmap. invalid load factor argument");
        }

        _hashMap = hashType switch
        {
            ClosedAddressingSeparateChaining => new ClosedAddressingSeparateChainingHashMap<K, V>(loadFactor),
            OpenAddressingLinearProbing => new LinearProbingHashMap<K, V>(loadFactor),
            OpenAddressingQuadraticProbing => new QuadraticProbingHashMap<K, V>(loadFactor),
            OpenAddressingDoubleHashing => throw new NotImplementedException(),
            OpenAddressingRobinHoodHashing => throw new NotImplementedException(),
            _ => throw new NotImplementedException("error. cannot create hashmap. invalid hash type"),
        };
    }

    public void Add(K key, V value)
    {
        _hashMap.Add(key, value);
    }

    public bool TryAdd(K key, V value)
    {
        return _hashMap.TryAdd(key, value);
    }

    public void Update(K key, V value)
    {
        _hashMap.Update(key, value);
    }

    public bool TryUpdate(K key, V value)
    {
        return _hashMap.TryUpdate(key, value);
    }

    public IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        return _hashMap.GetHashNodes();
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        return _hashMap.GetKeyValues();
    }

    public V Get(K key)
    {
        return _hashMap.Get(key);
    }

    public bool TryGet(K key, out V? value)
    {
        return _hashMap.TryGet(key, out value);
    }

    public V Remove(K key)
    {
        return _hashMap.Remove(key);
    }

    public bool TryRemove(K key, out V? value)
    {
        return _hashMap.TryRemove(key, out value);
    }
}