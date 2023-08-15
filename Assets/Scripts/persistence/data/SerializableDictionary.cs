using System;
using System.Collections.Generic;
using UnityEngine;

namespace persistence.data
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        public List<TKey> keys = new();
        public List<TValue> values = new();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            if (keys.Count != values.Count)
                throw new Exception(string.Format(
                    $"there are {keys.Count} keys and {values.Count} values after deserialization. " +
                    "Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++) Add(keys[i], values[i]);
        }
    }
}