using System;

namespace _Game.Develop.Infrastructure.DI
{
    public class Registration
    {
        private Func<DIContainer, object> _instanceCreator;
        private object _cachedInstance;

        public Registration(Func<DIContainer, object> instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public object GetInstance(DIContainer container)
        {
            if (_cachedInstance != null) return _cachedInstance;

            _cachedInstance = _instanceCreator(container);
            return _cachedInstance;
        }
    }
}