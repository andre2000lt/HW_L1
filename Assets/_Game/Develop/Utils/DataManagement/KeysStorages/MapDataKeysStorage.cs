using System;
using System.Collections.Generic;

namespace _Game.Develop.Utils.DataManagement.KeysStorages
{
    public class MapDataKeysStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> _typeToKey = new()
        {
            { typeof(PlayerData), "PlayerData" }
        };

        public string GetKeyFor<TData>() where TData : ISaveData
        {
            return _typeToKey[typeof(TData)];
        }
    }
}