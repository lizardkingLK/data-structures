namespace dataStructures.Core.NonLinear.Trees.Helpers;

public static class CompareHelper<T> where T : IComparable<T>
{
    public static bool FirstGreaterThanSecond(T valueA, T valueB)
    => GetComparissonValue(valueA, valueB) > 0;

    public static bool FirstLowerThanOrEqualsSecond(T valueA, T valueB)
    => GetComparissonValue(valueA, valueB) <= 0;

    public static bool FirstLowerThanSecond(T valueA, T valueB)
    => GetComparissonValue(valueA, valueB) < 0;

    public static bool FirstEqualsSecond(T valueA, T valueB)
    => GetComparissonValue(valueA, valueB) == 0;

    private static int GetComparissonValue(T valueA, T valueB)
    => valueA.CompareTo(valueB);
}