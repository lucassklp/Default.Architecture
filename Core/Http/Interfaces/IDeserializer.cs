using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Interfaces
{
    interface IDeserializer
    {
        DeserializedType Deserialize<DeserializedType, SerializedType>(SerializedType serialized);
    }
}
