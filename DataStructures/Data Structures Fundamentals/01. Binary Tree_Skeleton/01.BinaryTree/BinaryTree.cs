using System.Text;

namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder result = new StringBuilder();

            PreOrderWithIndent(this, indent, result);

            return result.ToString();
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            InOrderDFS(result, this);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            PostOrderOrderDFS(result, this);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            PreOrderDFS(result, this);

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            ForEachInOrderDFS(this, action);
        }

        private void ForEachInOrderDFS(IAbstractBinaryTree<T> tree, Action<T> action)
        {
            if (tree.LeftChild != null)
            {
                ForEachInOrderDFS(tree.LeftChild, action);
            }

            action.Invoke(tree.Value);

            if (tree.RightChild != null)
            {
                ForEachInOrderDFS(tree.RightChild, action);
            }
        }

        private void PreOrderWithIndent(IAbstractBinaryTree<T> tree, int indent, StringBuilder result)
        {
            result.AppendLine(new string(' ', indent) + tree.Value.ToString());

            indent += 2;

            if (tree.LeftChild != null)
            {
                PreOrderWithIndent(tree.LeftChild, indent, result);
            }

            if (tree.RightChild != null)
            {
                PreOrderWithIndent(tree.RightChild, indent, result);
            }
        }

        private void InOrderDFS(List<IAbstractBinaryTree<T>> result, IAbstractBinaryTree<T> tree)
        {

            if (tree.LeftChild != null)
            {
                InOrderDFS(result, tree.LeftChild);
            }

            result.Add(tree);

            if (tree.RightChild != null)
            {
                InOrderDFS(result, tree.RightChild);
            }
        }

        private void PreOrderDFS(List<IAbstractBinaryTree<T>> result, IAbstractBinaryTree<T> tree)
        {
            result.Add(tree);

            if (tree.LeftChild != null)
            {
                PreOrderDFS(result, tree.LeftChild);
            }

            if (tree.RightChild != null)
            {
                PreOrderDFS(result, tree.RightChild);
            }
        }

        private void PostOrderOrderDFS(List<IAbstractBinaryTree<T>> result, IAbstractBinaryTree<T> tree)
        {
            if (tree.LeftChild != null)
            {
                PostOrderOrderDFS(result, tree.LeftChild);
            }

            if (tree.RightChild != null)
            {
                PostOrderOrderDFS(result, tree.RightChild);
            }

            result.Add(tree);
        }
    }
}
