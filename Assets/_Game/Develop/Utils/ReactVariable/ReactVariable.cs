using System;
using System.Collections.Generic;

namespace _Game.Develop.IReadOnlyReactVariable
{
    public class ReactVariable<T> : IReadOnlyReactVariable<T> where T : IEquatable<T>
    {
        private readonly List<Subscriber<T, T>> _subscribers = new();
        private readonly List<Subscriber<T, T>> _toAdd = new();
        private readonly List<Subscriber<T, T>> _toRemove = new();

        public ReactVariable()
        {
            _value = default;
        }

        public ReactVariable(T value)
        {
            _value = value;
        }

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value)) return;

                T oldValue = _value;
                _value = value;

                Invoke(oldValue, value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
        {
            Subscriber<T, T> subscriber = new(action, Remove);
            _toAdd.Add(subscriber);

            return subscriber;
        }

        private void Remove(Subscriber<T, T> subscriber)
        {
            _toRemove.Add(subscriber);
        }

        private void Invoke(T oldValue, T newValue)
        {
            if (_toAdd.Count > 0)
            {
                _subscribers.AddRange(_toAdd);
                _toAdd.Clear();
            }

            if (_toRemove.Count > 0)
            {
                foreach (Subscriber<T, T> subscriber in _toRemove)
                {
                    _subscribers.Remove(subscriber);
                }

                _toRemove.Clear();
            }

            foreach (Subscriber<T, T> subscriber in _subscribers)
            {
                subscriber.Invoke(oldValue, newValue);
            }
        }
    }
}