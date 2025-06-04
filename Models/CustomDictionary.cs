using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metromap.Models
{
    public class CustomDictionary<TKey, TValue>
    {
        private CustomList<TKey> keys = new CustomList<TKey>();
        private CustomList<TValue> values = new CustomList<TValue>();

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            }
            if (ContainsKey(key))
            {
                throw new ArgumentException("An element with the same key already exists.", nameof(key));
            }

            keys.Add(key);
            values.Add(value);
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = IndexOfKey(key);
                if (index == -1)
                {
                    throw new KeyNotFoundException($"The key was not found.");
                }
                return values[index];
            }
            set
            {
                int index = IndexOfKey(key);
                if (index == -1)
                {
                    Add(key, value);
                }
                else
                {
                    values[index] = value;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return IndexOfKey(key) != -1;
        }

        public int Count => keys.Count;

        private int IndexOfKey(TKey key)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i].Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }
    }


}
