using System;
using System.Collections.Generic;

namespace _Game.Develop.Infrastructure.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Registration> _container = new();

        private List<Type> _requests = new();

        public void RegisterAsSingleton<T>(Func<DIContainer, T> instanceCreator)
        {
            Registration registration = new(container => instanceCreator(container));
            _container.Add(typeof(T), registration);
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new Exception($"Circle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            if (_container.TryGetValue(typeof(T), out Registration registration))
            {
                _requests.Remove(typeof(T));
                return (T)registration.GetInstance(this);
            }

            throw new Exception($"Can't resolve instance of type {typeof(T)}");
        }
    }
}