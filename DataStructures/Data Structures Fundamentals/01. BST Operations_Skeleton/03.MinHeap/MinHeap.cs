namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            CheckForEmptyList();

            var result = this._elements[0];

            Swap(0, this.Size - 1);

            this._elements.RemoveAt(this.Size - 1);

            HeapifyDown();

            return result;
        }

        private void HeapifyDown()
        {
            int index = 0;
            int leftChildIndex = GetLeftChildIndex(index);

            while (leftChildIndex <= this.Size - 1 && CompareElements(index, leftChildIndex) > 0)
            {

                int rightChildIndex = GetRightChildIndex(index);

                if (rightChildIndex < this.Size - 1 && CompareElements(rightChildIndex, leftChildIndex) < 0)
                {
                    leftChildIndex = rightChildIndex;
                }

                Swap(index, leftChildIndex);

                index = leftChildIndex;
                leftChildIndex = GetLeftChildIndex(index);
            }
        }

        public void Add(T element)
        {
            this._elements.Add(element);

            HeapifyUp();
        }

        public T Peek()
        {
            CheckForEmptyList();

            return this._elements[0];
        }

        private void HeapifyUp()
        {
            int addedIndex = this.Size - 1;
            int parentIndex = GetParentIndex(addedIndex);

            while (parentIndex >= 0 && CompareElements(addedIndex, parentIndex) < 0)
            {
                Swap(addedIndex, parentIndex);

                addedIndex = parentIndex;
                parentIndex = GetParentIndex(addedIndex);
            }
        }

        private void Swap(int addedIndex, int parentIndex)
        {
            var temp = this._elements[addedIndex];
            this._elements[addedIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = temp;
        }

        private int CompareElements(int addedIndex, int parentIndex)
        {
            return this._elements[addedIndex].CompareTo(this._elements[parentIndex]);
        }

        private int GetParentIndex(int addedIndex)
        {
            return (addedIndex - 1) / 2;
        }

        private void CheckForEmptyList()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private int GetRightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private int GetLeftChildIndex(int index)
        {
            return 2 * index + 1;
        }
    }
}
