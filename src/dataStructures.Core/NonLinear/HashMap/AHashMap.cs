using dataStructures.Core.Linear.Array;
using dataStructures.Core.Linear.LinkedList;
using dataStructures.Core.Shared;

namespace dataStructures.Core.NonLinear.HashMap;

public class AHashMap<K, V>
{
    public const int CAPACITY = 11;

    public const float LOAD_FACTOR = .75f;

    public int Capacity { get; private set; }

    public float LoadFactor { get; private set; }

    public int Size { get; private set; } = 0;

    private ADArray<ALinkedList<HashNode<K, V>>> buckets;

    public AHashMap()
    {
        Capacity = CAPACITY;
        LoadFactor = LOAD_FACTOR;

        buckets = new(Capacity);
        for (int i = 0; i < Capacity; i++)
        {
            buckets.Insert(i, new());
        }
    }

    public AHashMap(int capacity, float loadFactor)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(loadFactor, 0.1f, nameof(loadFactor));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(loadFactor, 1.0f, nameof(loadFactor));

        Capacity = capacity;
        LoadFactor = loadFactor;

        buckets = new(Capacity);
        for (int i = 0; i < Capacity; i++)
        {
            buckets.Insert(i, new());
        }
    }

    public void Insert(K key, V value)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));
        if (Size / Capacity > LoadFactor)
        {
            ReHash();
        }

        ALinkedList<HashNode<K, V>> bucketValues = GetBucketForKey(key);
        HashNode<K, V>? existingNode = Search(bucketValues, key);
        if (existingNode.HasValue)
        {
            throw new Exception("error. cannot insert. key already exist");
        }

        bucketValues.InsertToEnd(new(key, value));
        Size++;
    }

    public HashNode<K, V> Remove(K key)
    {
        ArgumentNullException.ThrowIfNull(key, nameof(key));

        ALinkedList<HashNode<K, V>> bucketValues = GetBucketForKey(key);
        HashNode<K, V>? existingNode = Search(bucketValues, key);
        if (!existingNode.HasValue)
        {
            throw new Exception("error. cannot remove. key does not exist");
        }

        bucketValues.RemoveLinkNodeAtFirstOccurrence(existingNode.Value);
        Size--;

        return existingNode.Value;
    }

    public HashNode<K, V>? Search(ALinkedList<HashNode<K, V>> bucketValues, K key)
    {
        if (bucketValues.Search(hashNode => hashNode.Key!.Equals(key), out HashNode<K, V> value))
        {
            return value;
        }

        return null;
    }
    
    public void Display()
    {
        Console.WriteLine("info. DISPLAYING VALUES OF HASHMAP/////////////////");
        for (int i = 0; i < Capacity; i++)
        {
            Console.WriteLine("bucket index {0} values are below", i);
            buckets.GetValue(i)!.Display();
            Console.WriteLine();
        }
    }

    private void ReHash()
    {
        int newCapacity = Capacity * 2;
        ADArray<ALinkedList<HashNode<K, V>>> tempBuckets = new(newCapacity);
        int i;
        for (i = 0; i < newCapacity; i++)
        {
            tempBuckets.Insert(i, new());
        }

        ALinkedList<HashNode<K, V>>? currentBucket;
        IEnumerator<HashNode<K, V>> currentBucketEnumerator;
        HashNode<K, V> currentHashNode;
        int newHashCode;
        ALinkedList<HashNode<K, V>> newBucket;
        for (i = 0; i < Capacity; i++)
        {
            currentBucket = buckets.GetValue(i);
            if (currentBucket == null)
            {
                throw new NullReferenceException(nameof(currentBucket));
            }

            currentBucketEnumerator = currentBucket.GetEnumerator();
            while (currentBucketEnumerator.MoveNext())
            {
                currentHashNode = currentBucketEnumerator.Current;
                newHashCode = GetHashCodeValue(currentHashNode.Key, newCapacity);
                newBucket = tempBuckets.GetValue(newHashCode)!;
                newBucket.InsertToEnd(currentHashNode);
            }
        }

        Capacity = newCapacity;
        buckets = tempBuckets;
    }

    private ALinkedList<HashNode<K, V>> GetBucketForKey(K key)
    {
        int hashCode = GetHashCodeValue(key);
        ALinkedList<HashNode<K, V>> bucketValues = buckets.GetValue(hashCode)
            ?? throw new NullReferenceException("error. bucket for key does not exist");

        return bucketValues;
    }

    private int GetHashCodeValue(K key, int? newCapacity = null)
    {
        newCapacity ??= Capacity;

        int value = -1;
        if (typeof(K) == typeof(int))
        {
            return GetHashCodeValueForInt(int.Parse(key?.ToString() ?? string.Empty), newCapacity.Value);
        }

        if (typeof(K) == typeof(string))
        {
            return GetHashCodeValueForString(key?.ToString() ?? string.Empty, newCapacity.Value);
        }

        return value;
    }

    private static int GetHashCodeValueForString(string key, int capacity)
    {
        int length = key.Length;
        int hashCode = 0;
        for (int i = 0; i < length; i++)
        {
            hashCode += key[i] * GetPowerForValue(31, length - i - 1);
        }

        return hashCode % capacity;
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