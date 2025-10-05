namespace dataStructures.Core.Linear.Lists.LinkedLists.Shared;

public static class Exceptions
{
    public static readonly Exception TargetNotFoundException = new ApplicationException("error. given target was not found");
    public static readonly Exception CannotRemoveException = new ApplicationException("error. cannot remove. item does not exist");
}