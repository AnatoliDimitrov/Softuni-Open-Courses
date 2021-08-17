using System.Collections.Generic;

namespace _02.MaxHeap
{
    using System;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            elements.Add(element);

            HeapifyUp();
        }

        private void HeapifyUp()
        {
            int addedElementIndex = this.Size - 1;
            int parentIndex = GetParentIndex(addedElementIndex);

            while (addedElementIndex > 0 && IsGreater(addedElementIndex, parentIndex))
            {
                Swap(addedElementIndex, parentIndex);

                addedElementIndex = parentIndex;
                parentIndex = GetParentIndex(addedElementIndex);
            }

        }

        private void Swap(int addedElementIndex, int parentIndex)
        {
            var temp = elements[addedElementIndex];
            elements[addedElementIndex] = elements[parentIndex];
            elements[parentIndex] = temp;
        }

        private bool IsGreater(int addedElementIndex, int parentIndex)
        {
            return elements[addedElementIndex].CompareTo(elements[parentIndex]) > 0;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        public T Peek()
        {
            IsEmpty();

            return elements[0];
        }

        private void IsEmpty()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException("No Elements!");
            }
        }
    }
}
