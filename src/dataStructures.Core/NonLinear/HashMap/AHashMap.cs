using dataStructures.Core.Linear.Array;
using dataStructures.Core.Linear.LinkedList;
using dataStructures.Core.Shared;

namespace dataStructures.Core.NonLinear.HashMap;

public class AHashMap<K, V>
{
    public const int CAPACITY = 11;

    public const float LOAD_FACTOR = .75f;

    public int Capacity { get; private set; } = CAPACITY;

    public float LoadFactor { get; private set; } = LOAD_FACTOR;

    public int Size { get; private set; } = 0;

    private ADArray<ALinkedList<HashNode<K, V>>> buckets;

    public AHashMap()
    {
        buckets = new(Capacity);
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
        if (Size / Capacity > LOAD_FACTOR)
        {
            ReHash();
        }

        ALinkedList<HashNode<K, V>> bucketValues = GetBucketForKey(key);
        HashNode<K, V>? existingNode = Search(key);
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
        HashNode<K, V>? existingNode = Search(key);
        if (!existingNode.HasValue)
        {
            throw new Exception("error. cannot remove. key does not exist");
        }

        bucketValues.RemoveLinkNodeAtFirstOccurrence(existingNode.Value);
        Size--;

        return existingNode.Value;
    }

    public HashNode<K, V>? Search(K key)
    {
        ALinkedList<HashNode<K, V>> bucketValues = GetBucketForKey(key);
        HashNode<K, V>? value = bucketValues.Search(hashNode => hashNode.Key!.Equals(key));

        return value;
    }

    public void Display()
    {
        for (int i = 0; i < Capacity; i++)
        {
            Console.WriteLine("bucket index {0} values are below", i);
            buckets.GetValue(i)!.Display();
            Console.WriteLine();
        }
    }

    private void ReHash()
    {

    }

    private ALinkedList<HashNode<K, V>> GetBucketForKey(K key)
    {
        int hashCode = GetHashCodeValue(key);
        ALinkedList<HashNode<K, V>> bucketValues = buckets.GetValue(hashCode)
            ?? throw new NullReferenceException("error. bucket for key does not exist");

        return bucketValues;
    }

    private int GetHashCodeValue(K key)
    {
        int value = -1;
        if (typeof(K) == typeof(int))
        {
            return GetHashCodeValueForInt(int.Parse(key?.ToString() ?? string.Empty));
        }

        if (typeof(K) == typeof(string))
        {
            return GetHashCodeValueForString(key?.ToString() ?? string.Empty);
        }

        return value;
    }

    private static int GetHashCodeValueForString(string key)
    {
        int length = key.Length;
        int hashCode = 0;
        for (int i = 0; i < length; i++)
        {
            hashCode += key[i] * GetPowerForValue(31, length - i - 1);
        }

        return hashCode;
    }

    private int GetHashCodeValueForInt(int key)
    {
        return key % Capacity;
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