namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}