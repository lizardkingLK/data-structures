using static dataStructures.Core.NonLinear.HashMaps.Shared.Utility;

namespace dataStructures.Core.NonLinear.HashMaps.Helpers;

public class HashingHelper<K>
{
    public int GetBucketIndex(K key, int capacity)
    {
        if (key is null)
        {
            throw new Exception("error. cannot call hash code. invalid key");
        }

        return GetAbsoluteValue(key.GetHashCode()) % capacity;
    }

    public Func<int> GetQuadraticProbing(K key, int capacity)
    {
        int index = GetBucketIndex(key, capacity);
        int iteration = 0;

        return () => (index + (iteration + iteration * iteration++) / 2) % capacity;
    }

    public Func<int> GetDoubleHashing(K key, int capacity)
    {
        int index = GetBucketIndex(key, capacity);
        int prime = GetPrimeNumber(capacity);
        int iteration = 0;

        return () => (index + iteration++ * (prime - index % prime) / 2) % capacity;
    }
}