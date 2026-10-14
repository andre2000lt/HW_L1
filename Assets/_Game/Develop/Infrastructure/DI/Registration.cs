using System;

namespace _Game.Develop.Infrastructure.DI
{
    public class Registration : IRegistrationOptions
    {
        private readonly Func<DIContainer, object> _instanceCreator;
        private object _cachedInstance;

        public Registration(Func<DIContainer, object> instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public bool IsNonLazy { get; private set; }

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null) return _cachedInstance;

            _cachedInstance = _instanceCreator(container);
            return _cachedInstance;
        }

        public void NonLazy()
        {
            IsNonLazy = true;
        }
    }
}