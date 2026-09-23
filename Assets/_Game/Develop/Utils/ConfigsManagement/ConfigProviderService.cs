using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Object = System.Object;

namespace _Game.Develop.Utils.ConfigServices
{
    public class ConfigProviderService
    {
        private readonly Dictionary<Type, Object> _configByType = new();
        private IConfigsLoader[] _configsLoaders;

        public ConfigProviderService(params IConfigsLoader[] configsLoaders)
        {
            _configsLoaders = configsLoaders;
        }

        public T GetConfig<T>() where T : class
        {
            if (_configByType.TryGetValue(typeof(T), out object config))
                return (T)config;

            throw new KeyNotFoundException($"Config {typeof(T)} not found");
        }

        public IEnumerator LoadAsync()
        {
            _configByType.Clear();

            foreach (IConfigsLoader loader in _configsLoaders)
            {
                yield return loader.LoadAsync(loadedConfigs =>
                    _configByType.AddRange(loadedConfigs));
            }
        }
    }
}