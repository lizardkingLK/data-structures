namespace dataStructures.Core.Linear.Arrays.Shared;

public static class Exceptions
{
    public static readonly ApplicationException InvalidIndexException = new("error. invalid index was given");
    public static readonly ApplicationException InvalidCapacityException = new("error. invalid capacity was given");
}