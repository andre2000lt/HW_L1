using System;

namespace _Game.Develop.IReadOnlyReactVariable
{
    public interface IReadOnlyReactVariable<T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T, T> action);
    }
}