namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            var node = new Node<T>(item);

            if (this._head == null)
            {
                this._head = node;
            }
            else
            {
                var current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = node;
            }

            this.Count++;
        }

        public bool Contains(T item)
        {
            ValidateQueueIsNotEmpty();

            var current = this._head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            ValidateQueueIsNotEmpty();

            var value = this._head.Value;

            this._head = this._head.Next;

            this.Count--;

            return value;
        }

        public T Peek()
        {
            ValidateQueueIsNotEmpty();

            return this._head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateQueueIsNotEmpty()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
        }
    }
}