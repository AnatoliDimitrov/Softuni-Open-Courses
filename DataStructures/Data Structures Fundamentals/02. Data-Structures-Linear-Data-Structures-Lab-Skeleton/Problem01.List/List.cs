namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List(int capacity = DEFAULT_CAPACITY)
        {
            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckBounderies(index);
                return _items[index];
            }
            set
            {
                CheckBounderies(index);
                _items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            CheckSize();

            _items[this.Count] = item;

            this.Count++;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) >= 0;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            CheckSize();
            CheckBounderies(index);

            for (int i = this.Count - 1; i >= index; i--)
            {
                _items[i + 1] = _items[i];
            }

            _items[index] = item;

            this.Count++;
        }

        public void RemoveAt(int index)
        {
            CheckBounderies(index);

            for (int i = index; i < this.Count; i++)
            {
                _items[i] = _items[i + 1];
            }

            _items[this.Count - 1] = default;

            this.Count--;
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index >= 0)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void CheckSize()
        {
            if (this.Count == _items.Length)
            {
                Grow();
            }
        }

        private void Grow()
        {
            var newArr = new T[_items.Length * 2];

            Array.Copy(_items, newArr, _items.Length);

            _items = newArr;
        }

        private void CheckBounderies(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index is out of the bounds of the array!");
            }
        }
    }
}