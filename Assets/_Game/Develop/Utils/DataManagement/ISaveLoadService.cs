using System;
using System.Collections;

namespace _Game.Develop.Utils.DataManagement
{
    public interface ISaveLoadService
    {
        IEnumerator Load<TData>(Action<TData> OnDataLoaded) where TData : ISaveData;
        IEnumerator Save<TData>(TData dataObject) where TData : ISaveData;
        IEnumerator Remove<TData>() where TData : ISaveData;
        IEnumerator IsExists<TData>(Action<bool> OnExistsResult) where TData : ISaveData;
    }
}