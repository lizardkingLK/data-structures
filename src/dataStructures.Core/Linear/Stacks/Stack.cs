using dataStructures.Core.Linear.Stacks.Abstractions;
using dataStructures.Core.Linear.Stacks.Enums;
using static dataStructures.Core.Linear.Stacks.Enums.StackTypeEnum;

namespace dataStructures.Core.Linear.Stacks;

public class Stack<T>(StackTypeEnum stackType, int size) : IStack<T>
{
    private readonly IStack<T> _stack = stackType switch
    {
        ArrayTyped => new ArrayStack<T>(size),
        LinkedListTyped => new LinkedListStack<T>(size),
        MonotonicTyped => new MonotonicStack<T>(size),
        _ => throw new NotImplementedException("error. cannot create. stack type not found"),
    };

    public Stack(int size) : this(ArrayTyped, size)
    {
    }

    public bool IsEmpty()
    {
        return _stack.IsEmpty();
    }

    public bool IsFull()
    {
        return _stack.IsFull();
    }

    public T Peek()
    {
        return _stack.Peek();
    }

    public T Pop()
    {
        return _stack.Pop();
    }

    public void Push(T item)
    {
        _stack.Push(item);
    }
}
