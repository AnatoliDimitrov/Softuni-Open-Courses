using System.Data;

namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            List<T> result = new List<T>();

            Dictionary<int, KeyValuePair<T, int>> values = new Dictionary<int, KeyValuePair<T, int>>();

            DFSTreePreOrder(this, 0, 0, values);

            result = values
                .Values
                .Select(v => v.Key)
                .ToList();

            return result;
        }

        private void DFSTreePreOrder(BinaryTree<T> tree, int level, int sideWay, Dictionary<int, KeyValuePair<T, int>> values)
        {
            if (tree == null)
            {
                return;
            }
            else
            {
                if (!values.ContainsKey(sideWay))
                {
                    values[sideWay] = new KeyValuePair<T, int>(tree.Value, level);
                }

                DFSTreePreOrder(tree.LeftChild, level + 1, sideWay - 1, values);
                DFSTreePreOrder(tree.RightChild, level + 1, sideWay + 1, values);
            }
        }
    }
}
