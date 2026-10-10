using System;
using System.Collections;

namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public abstract class DataProvider<TData> where TData : ISaveData
    {
        private ISaveLoadService _saveLoadService;

        private TData _data;

        protected DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public IEnumerator Save()
        {
            yield return _saveLoadService.Save(_data);
        }

        public IEnumerator Load()
        {
            yield return _saveLoadService.Load<TData>(result => _data = result);
        }

        public IEnumerator IsExists(Action<bool> onExistsResult)
        {
            yield return _saveLoadService.IsExists<TData>(result => onExistsResult?.Invoke(result));
        }

        public void Reset()
        {
            _data = GetOriginData();
        }

        protected abstract TData GetOriginData();
    }
}