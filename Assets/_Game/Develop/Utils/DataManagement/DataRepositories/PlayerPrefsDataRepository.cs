using System;
using System.Collections;
using UnityEngine;

namespace _Game.Develop.Utils.DataManagement.DataRepositories
{
    public class PlayerPrefsDataRepository : IDataRepository
    {
        public IEnumerator Read(string key, Action<string> onRead)
        {
            string data = PlayerPrefs.GetString(key);
            onRead?.Invoke(data);

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            PlayerPrefs.SetString(key, serializedData);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            PlayerPrefs.DeleteKey(key);

            yield break;
        }

        public IEnumerator Exists(string key, Action<bool> onExistsResult)
        {
            bool isFileExists = PlayerPrefs.HasKey(key);
            onExistsResult?.Invoke(isFileExists);

            yield break;
        }
    }
}