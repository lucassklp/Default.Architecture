namespace Core.Http.Interfaces
{
    interface IDeserializer
    {
        DeserializedType Deserialize<DeserializedType, SerializedType>(SerializedType serialized);
    }
}
