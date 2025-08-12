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
}