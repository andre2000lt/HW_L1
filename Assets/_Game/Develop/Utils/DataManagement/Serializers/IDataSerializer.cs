namespace _Game.Develop.Utils.DataManagement.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData dataObject);

        TData Deserialize<TData>(string serializedData);
    }
}