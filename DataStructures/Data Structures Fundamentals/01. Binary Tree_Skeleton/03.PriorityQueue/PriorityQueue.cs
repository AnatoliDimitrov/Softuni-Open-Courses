using System.Collections.Generic;

namespace _03.PriorityQueue
{
    using System;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public PriorityQueue()
        {
                this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public T Dequeue()
        {
            IsEmpty();

            var element = this.Peek();

            Swap(0, this.Size - 1);

            this.elements.RemoveAt(this.Size - 1);

            HeapifyDown();

            return element;
        }

        private void HeapifyDown()
        {
            int index = 0;
            int indexOfLeftChild = GetLeftChildIndex(index);

            while (indexOfLeftChild < this.Size && IsLess(index, indexOfLeftChild))
            {
                int indexOfRightChild = GetRightChildIndex(index);

                if (indexOfRightChild < this.Size && IsLess(indexOfLeftChild, indexOfRightChild))
                {
                    indexOfLeftChild = indexOfRightChild;
                }

                Swap(index, indexOfLeftChild);

                index = indexOfLeftChild;
                indexOfLeftChild = GetLeftChildIndex(index);
            }
        }

        public void Add(T element)
        {
            this.elements.Add(element);

            HeapifyUp();
        }

        private void HeapifyUp()
        {
            int indexOfAdded = this.Size - 1;
            int parentIndex = GetParentIndex(indexOfAdded);

            while (indexOfAdded > 0 && IsGreater(indexOfAdded, parentIndex))
            {
                Swap(indexOfAdded, parentIndex);

                indexOfAdded = parentIndex;
                parentIndex = GetParentIndex(indexOfAdded);
            }
        }

        private void Swap(int indexOfAdded, int parentIndex)
        {
            var temp = this.elements[indexOfAdded];
            this.elements[indexOfAdded] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsGreater(int indexOfAdded, int parentIndex)
        {
            return elements[indexOfAdded].CompareTo(elements[parentIndex]) > 0;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        public T Peek()
        {
            IsEmpty();

            return this.elements[0];
        }

        private void IsEmpty()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException("No elements!");
            }
        }

        private int GetRightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private bool IsLess(int index, int indexOfChild)
        {
            return elements[index].CompareTo(elements[indexOfChild]) < 0;
        }

        private int GetLeftChildIndex(int index)
        {
            return 2 * index + 1;
        }
    }
}
