using dataStructures.Core.Linear.Array;
using dataStructures.Core.Shared;

namespace dataStructures.Core.NonLinear.HashMap;

public class HashMap<K, V>
{
    public const int CAPACITY = 11;

    public const float LOAD_FACTOR = .75f;

    public int Capacity { get; private set; }

    public float LoadFactor { get; private set; }

    public int Size { get; private set; } = 0;

    private DynamicArray<Linear.LinkedList.LinkedList<HashNode<K, V>>> buckets;

    public HashMap()
    {
        Capacity = CAPACITY;
        LoadFactor = LOAD_FACTOR;

        buckets = new(Capacity);
        for (int i = 0; i < Capacity; i++)
        {
            buckets.Add(i, new());
        }
    }

    public HashMap(int capacity, float loadFactor)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(loadFactor, 0.1f, nameof(loadFactor));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(loadFactor, 1.0f, nameof(loadFactor));

        Capacity = capacity;
        LoadFactor = loadFactor;

        buckets = new(Capacity);
        for (int i = 0; i < Capacity; i++)
        {
            buckets.Add(i, new());
        }
    }

    public void Insert(K key, V value)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        if (Size / Capacity > LoadFactor)
        {
            ReHash();
        }

        if (TryGetValue(key, out var bucketValues, out _))
        {
            throw new Exception("error. cannot insert. key already exist");
        }

        bucketValues!.InsertToEnd(new(key, value));
        Size++;
    }

    public bool TryInsert(K key, V value)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        if (key.Equals(default))
        {
            return false;
        }
        
        if (Size / Capacity > LoadFactor)
        {
            ReHash();
        }

        if (TryGetValue(key, out Linear.LinkedList.LinkedList<HashNode<K, V>>? bucketValues, out _))
        {
            return false;
        }

        bucketValues!.InsertToEnd(new(key, value));
        Size++;

        return true;
    }

    public HashNode<K, V> Remove(K key)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        if (!TryGetValue(key, out Linear.LinkedList.LinkedList<HashNode<K, V>>? bucketValues, out _))
        {
            throw new Exception("error. cannot remove. key does not exist");
        }

        HashNode<K, V>? existingNode = HashMap<K, V>.Search(bucketValues!, key);
        bucketValues!.RemoveLinkNodeAtFirstOccurrence(existingNode!);
        Size--;

        return existingNode!;
    }

    public void Display()
    {
        Console.WriteLine("info. DISPLAYING VALUES OF HASHMAP/////////////////");
        for (int i = 0; i < Capacity; i++)
        {
            Console.WriteLine("bucket index {0} values are below", i);
            buckets.Get(i)!.Display();
            Console.WriteLine();
        }
    }

    public bool TryGetValue(K key, out V? value)
    {
        value = default;

        Linear.LinkedList.LinkedList<HashNode<K, V>> bucketValues = GetBucketForKey(key);
        HashNode<K, V>? existingNode = HashMap<K, V>.Search(bucketValues, key);
        if (existingNode != null)
        {
            value = existingNode.Value;
            return true;
        }

        return false;
    }

    private bool TryGetValue(K key, out Linear.LinkedList.LinkedList<HashNode<K, V>>? bucketValues, out V? value)
    {
        value = default;

        bucketValues = GetBucketForKey(key);
        HashNode<K, V>? existingNode = HashMap<K, V>.Search(bucketValues, key);
        if (existingNode != null)
        {
            value = existingNode.Value;
            return true;
        }

        return false;
    }

    private static HashNode<K, V>? Search(Linear.LinkedList.LinkedList<HashNode<K, V>> bucketValues, K key)
    {
        if (bucketValues.Search(hashNode => hashNode.Key!.Equals(key), out HashNode<K, V>? value))
        {
            return value;
        }

        return null;
    }

    private void ReHash()
    {
        int newCapacity = Capacity * 2;
        DynamicArray<Linear.LinkedList.LinkedList<HashNode<K, V>>> tempBuckets = new(newCapacity);
        int i;
        for (i = 0; i < newCapacity; i++)
        {
            tempBuckets.Add(i, new());
        }

        Linear.LinkedList.LinkedList<HashNode<K, V>>? currentBucket;
        IEnumerator<HashNode<K, V>> currentBucketEnumerator;
        HashNode<K, V> currentHashNode;
        int newHashCode;
        Linear.LinkedList.LinkedList<HashNode<K, V>> newBucket;
        for (i = 0; i < Capacity; i++)
        {
            currentBucket = buckets.Get(i);
            if (currentBucket == null)
            {
                throw new NullReferenceException(nameof(currentBucket));
            }

            currentBucketEnumerator = currentBucket.GetEnumerator();
            while (currentBucketEnumerator.MoveNext())
            {
                currentHashNode = currentBucketEnumerator.Current;
                newHashCode = GetHashCodeValue(currentHashNode.Key, newCapacity);
                newBucket = tempBuckets.Get(newHashCode)!;
                newBucket.InsertToEnd(currentHashNode);
            }
        }

        Capacity = newCapacity;
        buckets = tempBuckets;
    }

    private Linear.LinkedList.LinkedList<HashNode<K, V>> GetBucketForKey(K key)
    {
        int hashCode = GetHashCodeValue(key);
        if (!buckets.TryGet(hashCode, out Linear.LinkedList.LinkedList<HashNode<K, V>>? bucketValues))
        {
            throw new NullReferenceException("error. bucket for key does not exist");
        }

        return bucketValues!;
    }

    private int GetHashCodeValue(K key, int? newCapacity = null)
    {
        newCapacity ??= Capacity;

        Type typeofK = typeof(K);
        if (typeofK == typeof(int))
        {
            return GetHashCodeValueForInt(int.Parse(key?.ToString() ?? string.Empty), newCapacity.Value);
        }

        return GetHashCodeValueForString(key?.ToString() ?? string.Empty, newCapacity.Value);
    }

    private static int GetHashCodeValueForString(string key, int capacity)
    {
        int length = key.Length;
        long hashCode = 0;
        for (int i = 0; i < length; i++)
        {
            hashCode += key[i] * GetPowerForValue(31, length - i - 1);
        }

        return GetAbsoluteHashCodeValue((int)hashCode % capacity);
    }

    private static int GetAbsoluteHashCodeValue(int value)
    {
        int signBit = value >> 31;

        return (value ^ signBit) - signBit;
    }

    private static int GetHashCodeValueForInt(int key, int capacity)
    {
        return key % capacity;
    }

    private static int GetPowerForValue(int baseValue, int powerValue, int currentValue = 1)
    {
        if (powerValue == 0)
        {
            return currentValue;
        }

        return GetPowerForValue(baseValue, powerValue - 1, currentValue * baseValue);
    }

}
