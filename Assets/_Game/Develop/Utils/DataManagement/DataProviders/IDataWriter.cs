namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}