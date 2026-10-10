using System;
using System.Collections;
using _Game.Develop.Utils.DataManagement.DataRepositories;
using _Game.Develop.Utils.DataManagement.KeysStorages;
using _Game.Develop.Utils.DataManagement.Serializers;

namespace _Game.Develop.Utils.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeysStorage _keysStorage;
        private readonly IDataRepository _repository;

        public SaveLoadService(IDataSerializer serializer, IDataKeysStorage keysStorage, IDataRepository repository)
        {
            _keysStorage = keysStorage;
            _serializer = serializer;
            _repository = repository;
        }

        public IEnumerator Load<TData>(Action<TData> OnDataLoaded) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();
            string serializedData = "";

            yield return _repository.Read(key, data => serializedData = data);

            TData dataObject = _serializer.Deserialize<TData>(serializedData);
            OnDataLoaded?.Invoke(dataObject);
        }

        public IEnumerator Save<TData>(TData dataObject) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(dataObject);
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _repository.Write(key, serializedData);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _repository.Remove(key);
        }

        public IEnumerator IsExists<TData>(Action<bool> OnExistsResult) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _repository.Exists(key, result => OnExistsResult?.Invoke(result));
        }
    }
}