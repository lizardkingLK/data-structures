namespace dataStructures.Core.NonLinear.HashMaps.Shared;

public static class Utility
{
    public static int GetAbsoluteValue(int value)
    {
        int signBitMask = value >> 31;

        value = (value ^ signBitMask) - signBitMask;

        return value;
    }

    public static int GetPrimeNumber(int number)
    {
        if (number <= 1)
        {
            return 1;
        }

        for (int i = number - 1; i >= 1; i--)
        {
            if (IsPrime(i))
            {
                return i;
            }
        }

        return 1;
    }

    private static int GetSquareRoot(int number)
    {
        int squareRoot = 0;
        for (int i = 0; i < number; i++)
        {
            if (i * i == number)
            {
                return i;
            }

            squareRoot = i;
        }

        return squareRoot;
    }

    private static bool IsPrime(int number)
    {
        int squareRoot = GetSquareRoot(number);
        for (int i = 2; i <= squareRoot; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}