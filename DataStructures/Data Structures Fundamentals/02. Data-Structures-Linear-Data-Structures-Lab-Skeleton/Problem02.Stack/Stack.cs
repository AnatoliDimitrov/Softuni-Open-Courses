namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            var node = new Node<T>(item);

            if (_top == null)
            {
                _top = node;
            }
            else
            {
                node.Next = _top;
                _top = node;
            }

            this.Count++;
        }

        public T Pop()
        {
            ValidateStackIsNotEmpty();

            var toReturn = this._top;

            this._top = this._top.Next;

            this.Count--;

            return toReturn.Value;
        }

        public T Peek()
        {
            ValidateStackIsNotEmpty();

            return this._top.Value;
        }

        public bool Contains(T item)
        {
            var current = this._top;

            while (current.Next != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            if (current.Value.Equals(item))
            {
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            ValidateStackIsNotEmpty();

            var current = _top;

            while (current.Next != null)
            {
                yield return current.Value;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();



        private void ValidateStackIsNotEmpty()
        {
            if (_top == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
        }
    }
}