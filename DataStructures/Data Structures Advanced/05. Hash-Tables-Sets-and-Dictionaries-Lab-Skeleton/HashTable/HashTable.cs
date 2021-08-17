using System.Dynamic;

namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private LinkedList<KeyValue<TKey, TValue>>[] slots;
        private const int _capacity = 16;
        public int Count { get; private set; }

        public int Capacity
        {
            get => this.slots.Length;
        }

        public HashTable(int capacity = _capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();

            int index = GetIndexByKey(key);

            if (slots[index] == null)
            {
                slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            else
            {
                foreach (var kvp in slots[index])
                {
                    if (kvp.Key.Equals(key))
                    {
                        throw new ArgumentException();
                    }
                }
            }

            slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            Count++;
        }

        private int GetIndexByKey(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % this.Capacity;
        }

        private void GrowIfNeeded()
        {
            double currentFillFactor = (this.Count + 1) / (double) this.Capacity;
            if (currentFillFactor >= 0.75)
            {
                this.slots = Grow();
            }
        }

        private LinkedList<KeyValue<TKey, TValue>>[] Grow()
        {
            var newArray = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var list in this.slots)
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        newArray.Add(item.Key, item.Value);
                    }
                }
            }

            return newArray.slots;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            GrowIfNeeded();

            int index = GetIndexByKey(key);

            if (slots[index] == null)
            {
                slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
                //slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            }
            else
            {
                foreach (var kvp in slots[index])
                {
                    if (kvp.Key.Equals(key))
                    {
                        kvp.Value = value;
                        return false;
                    }
                }
            }

            slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            Count++;
            return true;
        }

        public TValue Get(TKey key)
        {
            var kvp = Find(key);

            if (kvp != null)
            {
                return kvp.Value;
            }

            throw new KeyNotFoundException();
        }

        public TValue this[TKey key]
        {
            get => this.Get(key);
            set => this.AddOrReplace(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var kvp = Find(key);

            if (kvp == null)
            {
                value = default;
                return false;
            }

            value = kvp.Value;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = GetIndexByKey(key);

            if (this.slots[index] != null)
            {
                foreach (var keyValue in this.slots[index])
                {
                    if (keyValue.Key.Equals(key))
                    {
                        return keyValue;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            var kvp = Find(key);

            if (kvp != null)
            {
                return true;
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            int index = GetIndexByKey(key);

            if (this.slots[index] != null)
            {
                var kvp = Find(key);

                if (kvp != null)
                {
                    this.Count--;
                    return this.slots[index].Remove(kvp);
                }

                return false;
            }

            return false;
        }

        public void Clear()
        {
           this.slots = new LinkedList<KeyValue<TKey, TValue>>[_capacity];
           this.Count = 0;
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (var linkedList in this.slots)
                {
                    if (linkedList != null)
                    {
                        foreach (var keyValue in linkedList)
                        {
                            yield return keyValue.Key;
                        }
                    }
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var linkedList in this.slots)
                {
                    if (linkedList != null)
                    {
                        foreach (var keyValue in linkedList)
                        {
                            yield return keyValue.Value;
                        }
                    }
                }
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (var keyValue in linkedList)
                    {
                        yield return keyValue;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
