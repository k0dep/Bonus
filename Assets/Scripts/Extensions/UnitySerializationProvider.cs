using System;
using System.Text;
using Poster;
using UnityEngine;

namespace Extensions
{
    public class UnitySerializationProvider : ISerializationProvider
    {
        public byte[] Serialize(object source)
        {
            return Encoding.Unicode.GetBytes(JsonUtility.ToJson(source));
        }

        public T Deserialize<T>(byte[] data)
        {
            return (T)Deserialize(typeof(T), data);
        }

        public object Deserialize(Type type, byte[] data)
        {
            return JsonUtility.FromJson(Encoding.Unicode.GetString(data), type);
        }
    }
}