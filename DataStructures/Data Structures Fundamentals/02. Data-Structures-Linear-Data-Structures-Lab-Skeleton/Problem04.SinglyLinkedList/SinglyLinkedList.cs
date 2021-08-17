namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = new Node<T>(item, this._head);
            this._head = node;
            this.Count++;
        }

        public void AddLast(T item)
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
        public T RemoveFirst()
        {
            CheckForEmptyList();

            var value = this._head.Value;

            this._head = this._head.Next;

            this.Count--;

            return value;
        }

        public T RemoveLast()
        {
            CheckForEmptyList();

            var current = this._head;

            T value;

            this.Count--;

            if (current.Next == null)
            {
                value = current.Value;

                this._head = null;

                return value;
            }
            else
            {
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }

                value = current.Next.Value;

                current.Next = null;

                return value;
            }
        }

        public T GetFirst()
        {
            CheckForEmptyList();

            return this._head.Value;
        }

        public T GetLast()
        {
            CheckForEmptyList();

            var current = this._head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Value;
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

        private void CheckForEmptyList()
        {
            if (this._head == null)
            {
                throw new InvalidOperationException("List is empty!");
            }
        }
    }
}