using System.Collections.Generic;

namespace _02.LowestCommonAncestor
{
    using System;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> leftChild, BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            SetParent();
        }

        private void SetParent()
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            T result = default;

            BinaryTree<T> firstTree = FindFirst(this, first, null);
            BinaryTree<T> secondTree = FindSecond(this, second, null);

            var parentsValuesOfFirst = new List<T>();
            var parentsValuesOfSecond = new List<T>();

            var current = firstTree;

            while (current.Parent != null)
            {
                parentsValuesOfFirst.Add(current.Value);
                current = current.Parent;
            }

            parentsValuesOfFirst.Add(current.Value);

            current = secondTree;

            while (current.Parent != null)
            {
                parentsValuesOfSecond.Add(current.Value);
                current = current.Parent;
            }

            parentsValuesOfSecond.Add(current.Value);

            result = FindCommon(parentsValuesOfFirst, parentsValuesOfSecond);

            return result;
        }

        private T FindCommon(List<T> first, List<T> second)
        {
            T result = default;

            for (int i = 0; i < first.Count; i++)
            {
                if (second.Contains(first[i]))
                {
                    result = first[i];
                    break;
                }
            }

            return result;
        }

        private BinaryTree<T> FindSecond(BinaryTree<T> tree, T second, BinaryTree<T> result)
        {
            if (tree.LeftChild != null)
            {
                result = FindFirst(tree.LeftChild, second, result);
            }

            if (tree.RightChild != null)
            {
                result = FindFirst(tree.RightChild, second, result);
            }

            if (tree.Value.CompareTo(second) == 0)
            {
                result = tree;
                return result;
            }

            return result;
        }

        private BinaryTree<T> FindFirst(BinaryTree<T> tree, T first, BinaryTree<T> result)
        {
            if (tree.LeftChild != null)
            {
                result = FindFirst(tree.LeftChild, first, result);
            }

            if (tree.RightChild != null)
            {
                result = FindFirst(tree.RightChild, first, result);
            }

            if (tree.Value.CompareTo(first) == 0)
            {
                result = tree;
                return result;
            }

            return result;
        }
    }
}
