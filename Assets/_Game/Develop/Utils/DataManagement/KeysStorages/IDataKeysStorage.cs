namespace _Game.Develop.Utils.DataManagement.KeysStorages
{
    public interface IDataKeysStorage
    {
        string GetKeyFor<TData>() where TData : ISaveData;
    }
}