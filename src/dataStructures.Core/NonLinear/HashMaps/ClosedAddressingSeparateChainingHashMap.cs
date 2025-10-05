using dataStructures.Core.Linear.Arrays;
using dataStructures.Core.Linear.Lists.LinkedLists;
using dataStructures.Core.NonLinear.HashMaps.Abstractions;
using dataStructures.Core.NonLinear.HashMaps.Helpers;
using dataStructures.Core.NonLinear.HashMaps.State;
using static dataStructures.Core.NonLinear.HashMaps.Shared.Constants;

namespace dataStructures.Core.NonLinear.HashMaps;

internal class ClosedAddressingSeparateChainingHashMap<K, V>(float loadFactor) : IHashMap<K, V>
{
    private DynamicArray<DoublyLinkedList<HashNode<K, V>>> _buckets = new();

    private readonly HashingHelper<K> _hashing = new();

    private readonly float _loadFactor = loadFactor;

    public int Capacity { get; private set; } = INITIAL_CAPACITY;

    public int Size { get; private set; }

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            throw new Exception("error. cannot add value. key already contain");
        }

        bucket!.AddToTail(new(key, value));
        Size++;

        if (Size / Capacity >= _loadFactor)
        {
            ReHash();
        }
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            return false;
        }

        bucket!.AddToTail(new(key, value));
        Size++;

        if (Size / Capacity >= _loadFactor)
        {
            ReHash();
        }

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

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    public IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        foreach (DoublyLinkedList<HashNode<K, V>>? bucket in _buckets.Values)
        {
            if (bucket is null)
            {
                continue;
            }

            foreach (HashNode<K, V> hashNode in bucket.ValuesHeadToTail)
            {
                yield return hashNode;
            }
        }
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        foreach ((K key, V value, _, _) in GetHashNodes())
        {
            yield return new(key, value);
        }
    }

    public V Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot get value. key does not contain");
        }

        return hashNode!.Value;
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

    public V Remove(K key)
    {
        if (!ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot remove value. key does not contain");
        }

        bucket!.Remove(hashNode!);

        return hashNode!.Value;
    }

    public bool TryRemove(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        bucket!.Remove(hashNode!);

        value = hashNode!.Value;

        return true;
    }

    private bool ContainsKey(
        K key,
        out DoublyLinkedList<HashNode<K, V>>? bucket,
        out HashNode<K, V>? value)
    {
        value = default;

        int index = _hashing.GetBucketIndex(key, Capacity);
        bool doesBucketContain = _buckets.TryGetValue(index, out bucket);
        bool containsKey = false;
        if (doesBucketContain)
        {
            containsKey = bucket!.TryGetValue(value => value.Key!.Equals(key), out value);
        }
        else
        {
            bucket = _buckets.Insert(index, new());
        }

        return containsKey;
    }

    private void ReHash()
    {
        Capacity *= 2;
        DynamicArray<DoublyLinkedList<HashNode<K, V>>> tempBuckets = new(Capacity);
        int index;
        foreach ((K key, V value, _, _) in GetHashNodes())
        {
            index = _hashing.GetBucketIndex(key, Capacity);
            if (!tempBuckets.TryGetValue(index, out DoublyLinkedList<HashNode<K, V>>? bucket))
            {
                bucket = tempBuckets.Insert(index, new());
            }

            bucket!.AddToTail(new(key, value));
        }

        _buckets = tempBuckets;
    }
}