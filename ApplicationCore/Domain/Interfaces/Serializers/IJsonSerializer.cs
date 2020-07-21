namespace ApplicationCore.Domain.Interfaces.Serializers
{
    public interface IJsonSerializer<T>
    {
        byte[] ObjectToByteArray(object data);

        string GetJsonStringFromByteArray(byte[] data);

        T GetDeserializedObject(string data);
    }
}