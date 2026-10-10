using Newtonsoft.Json;

namespace _Game.Develop.Utils.DataManagement.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        public string Serialize<TData>(TData dataObject)
        {
            JsonSerializerSettings settings = new()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            };

            return JsonConvert.SerializeObject(dataObject);
        }

        public TData Deserialize<TData>(string serializedData)
        {
            JsonSerializerSettings settings = new()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            return JsonConvert.DeserializeObject<TData>(serializedData, settings);
        }
    }
}