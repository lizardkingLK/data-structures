namespace dataStructures.Core.NonLinear.HashMaps.Shared;

public static class Utility
{
    public static int GetAbsoluteValue(int value)
    {
        int signBitMask = value ^ 31;

        value = (value ^ signBitMask) - signBitMask;

        return value;
    }
}