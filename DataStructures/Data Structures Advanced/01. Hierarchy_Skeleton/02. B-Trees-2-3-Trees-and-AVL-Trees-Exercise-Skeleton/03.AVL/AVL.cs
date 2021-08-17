
using System.ComponentModel;

namespace _03.AVL
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public bool Contains(T item)
        {
            var node = this.Search(this.Root, item);
            return node != null;
        }

        public void Insert(T item)
        {
            this.Root = this.Insert(this.Root, item);
        }

        public void Delete(T element)
        {
            if (this.Root == null)
            {
                return;
            }

            this.Root = RemoveNode(this.Root, element);
            UpdateHeight(this.Root);
        }

        private Node<T> RemoveNode(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = RemoveNode(node.Left, item);
                UpdateHeight(node);
            }
            else if (cmp > 0)
            {
                node.Right = RemoveNode(node.Right, item);
                UpdateHeight(node);
            }
            else if (cmp == 0)
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if (node.Left != null && node.Right == null)
                {
                    return node.Left;
                }
                else if (node.Left == null && node.Right != null)
                {
                    return node.Right;
                }
                else
                {
                    if (node.Left.Height > node.Right.Height)
                    {
                        var max = FindMax(node.Left);
                        node.Value = max.Value;
                        node.Left = RemoveNode(node.Left, max.Value);
                        UpdateHeight(node.Left);
                        UpdateHeight(node);
                    }
                    else
                    {
                        var min = FindMin(node.Right);
                        node.Value = min.Value;
                        node.Right = RemoveNode(node.Right, min.Value);
                        UpdateHeight(node.Right);
                        UpdateHeight(node);
                    }
                }

            }


            return Balance(node);
        }


        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }

            Node<T> min = FindMin(this.Root);

            Delete(min.Value);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            node = Balance(node);
            UpdateHeight(node);
            return node;
        }

        private Node<T> Balance(Node<T> node)
        {
            var balance = Height(node.Left) - Height(node.Right);
            if (balance > 1)
            {
                var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
                if (childBalance < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }
            else if (balance < -1)
            {
                var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
                if (childBalance > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }

            return node;
        }

        private void UpdateHeight(Node<T> node)
        {
            if (node != null)
            {
                node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            }
        }

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Height(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            UpdateHeight(node);

            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            UpdateHeight(node);

            return right;
        }

        private Node<T> FindMin(Node<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        private Node<T> FindMax(Node<T> node)
        {
            if (node.Right == null)
            {
                return node;
            }

            return FindMax(node.Right);
        }
    }
}
