using ApplicationCore.Domain.Interfaces.Serializers;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Serializers
{
    public class NewtonsoftSerializer<T> : IJsonSerializer<T>
    {
        public T GetDeserializedObject(string data)
        {
            return JsonConvert
                .DeserializeObject<T>(data);
        }

        public string GetJsonStringFromByteArray(byte[] data)
        {
            string jsonString = Encoding.UTF8
                .GetString(data);

            return jsonString;
        }

        public byte[] ObjectToByteArray(object data)
        {
            return Encoding.UTF8
                .GetBytes(JsonConvert.SerializeObject(data));
        }
    }
}