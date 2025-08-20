using dataStructures.Core.Linear.Arrays;
using dataStructures.Core.NonLinear.HashMaps.Abstractions;
using dataStructures.Core.NonLinear.HashMaps.Helpers;
using dataStructures.Core.NonLinear.HashMaps.State;
using static dataStructures.Core.NonLinear.HashMaps.Shared.Constants;

namespace dataStructures.Core.NonLinear.HashMaps;

public class QuadraticProbingHashMap<K, V>(float loadFactor) : IHashMap<K, V>
{
    private readonly float _loadFactor = loadFactor;

    private readonly HashingHelper<K> _hashing = new();

    private DynamicArray<HashNode<K, V>?> _buckets = new();

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public int Capacity { get; private set; } = INITIAL_CAPACITY;

    public int Size { get; private set; }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            throw new Exception("error. cannot add value. key already contain");
        }

        _buckets.Add(index, new(key, value));
        Size++;

        if (Size >= Capacity)
        {
            ReHash();
        }
    }

    public V Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? value))
        {
            throw new Exception("error. cannot get value. key does not contain");
        }

        return value!.Value;
    }

    public IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        foreach (HashNode<K, V>? bucket in _buckets.Values)
        {
            if (bucket is not null)
            {
                yield return bucket;
            }
        }
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        foreach ((K key, V value) in GetHashNodes())
        {
            yield return new(key, value);
        }
    }

    public V Remove(K key)
    {
        if (!ContainsKey(key, out int index, out HashNode<K, V>? value))
        {
            throw new Exception("error. cannot remove value. key does not contain");
        }

        _buckets.Remove(index);

        return value!.Value;
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            return false;
        }

        _buckets.Add(index, new(key, value));
        Size++;

        if (Size / _loadFactor >= Capacity)
        {
            ReHash();
        }

        return true;
    }

    public bool TryGet(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        value = hashNode!.Value;

        return true;
    }

    public bool TryRemove(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out int index, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        _buckets.Remove(index);

        value = hashNode!.Value;

        return true;
    }

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    public void Update(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot update value. key does not contain");
        }

        hashNode!.Value = value;
    }

    private bool ContainsKey(
        K key,
        out int validIndex,
        out HashNode<K, V>? value,
        Func<int>? GetNextIndex = null)
    {
        GetNextIndex ??= _hashing.GetQuadraticProbing(key, Capacity);

        validIndex = GetNextIndex();

        bool doesBucketContain = _buckets.TryGet(validIndex, out value);

        if (doesBucketContain && value!.Key!.Equals(key))
        {
            return true;
        }
        else if (value is null)
        {
            return false;
        }

        return ContainsKey(key, out validIndex, out value, GetNextIndex);
    }

    private void ReHash()
    {
        Capacity *= 2;
        DynamicArray<HashNode<K, V>?> tempBuckets = new(Capacity);
        int index;
        foreach (HashNode<K, V> bucket in GetHashNodes())
        {
            index = _hashing.GetBucketIndex(bucket.Key, Capacity);
            while (tempBuckets.TryGet(index, out HashNode<K, V>? value))
            {
                index = (index + 1) % Capacity;
            }

            tempBuckets.Add(index, bucket);
        }

        _buckets = tempBuckets;
    }
}