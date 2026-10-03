using System;
using System.Collections.Generic;

namespace _Game.Develop.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private List<Type> _requests = new();
        private DIContainer _parent;

        public DIContainer(DIContainer parent = null)
        {
            _parent = parent;
        }

        public void RegisterAsSingleton<T>(Func<DIContainer, T> instanceCreator)
        {
            if (IsAlreadyRegistered<T>())
            {
                throw new Exception($"{typeof(T)} already registered");
            }

            Registration registration = new(container => instanceCreator(container));
            _container.Add(typeof(T), registration);
        }

        public bool IsAlreadyRegistered<T>()
        {
            if (_container.ContainsKey(typeof(T)))
            {
                return true;
            }

            if (_parent != null)
            {
                return _parent.IsAlreadyRegistered<T>();
            }

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new Exception($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            if (_container.TryGetValue(typeof(T), out Registration registration))
            {
                _requests.Remove(typeof(T));
                return (T)registration.GetInstance(this);
            }

            if (_parent != null)
            {
                return _parent.Resolve<T>();
            }

            throw new Exception($"Can't resolve instance of type {typeof(T)}");
        }
    }
}