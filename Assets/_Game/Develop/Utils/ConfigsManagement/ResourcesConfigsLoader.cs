using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Develop.Utils.ConfigServices.Configs;
using UnityEngine;

namespace _Game.Develop.Utils.ConfigServices
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private Dictionary<Type, string> _configPathByType = new()
        {
            { typeof(TestConfig), "Configs/TestConfig" }
        };


        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configPath in _configPathByType)
            {
                ScriptableObject config = Resources.Load<ScriptableObject>(configPath.Value);

                if (config != null)
                    loadedConfigs.Add(configPath.Key, config);

                yield return null;
            }

            onConfigLoaded?.Invoke(loadedConfigs);
        }
    }
}