using System.ComponentModel;

namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> 
        : IBinarySearchTree<T> where T : IComparable
    {
        private const bool RED = true;
        private const bool BLACK = false;

        private Node root;

        public int Count => GetCount(this.root);

        public void Insert(T element)
        {
            this.root = Insert(this.root, element);
            this.root.Color = BLACK;
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            int comparator = node.Value.CompareTo(element);

            if (comparator > 0)
            {
                node.Left = Insert(node.Left, element);
            }
            else if (comparator < 0)
            {
                node.Right = Insert(node.Right, element);
            }

            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                node = FlipColors(node);
            }

            UpdateCount(node);

            return node;
        }

        private Node FlipColors(Node node)
        {
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;
            node.Color = RED;

            return node;
        }

        private Node RotateRight(Node node)
        {
            var newNode = node.Left;
            node.Left = newNode.Right;
            newNode.Right = node;

            newNode.Color = node.Color;
            node.Color = RED;
            UpdateCount(node);

            return newNode;
        }

        private Node RotateLeft(Node node)
        {
            var newNode = node.Right;
            node.Right = newNode.Left;
            newNode.Left = node;

            newNode.Color = node.Color;
            node.Color = RED;
            UpdateCount(node);

            return newNode;
        }

        public T Select(int rank)
        {
            Node result = Select(this.root, rank);

            if (result == null)
            {
                throw new ArgumentException();
            }

            return result.Value;
        }

        public int Rank(T element)
        {
            return Rank(this.root, element);
        }

        public bool Contains(T element)
        {
            var current = this.root;

            while (current != null)
            {
                int comparator = element.CompareTo(current.Value);

                if (comparator > 0)
                {
                    current = current.Right;
                }
                else if (comparator < 0)
                {
                    current = current.Left;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            var tree = new RedBlackTree<T>();
            tree.root = Search(this.root, element);
            return tree;
        }

        private Node Search(Node node, T element)
        {
            var current = node;

            while (current != null)
            {
                int comparator = element.CompareTo(current.Value);

                if (comparator > 0)
                {
                    current = current.Right;
                }
                else if (comparator < 0)
                {
                    current = current.Left;
                }
                else
                {
                    return current;
                }
            }

            return null;
        }

        public void DeleteMin()
        {
            this.root = DeleteMin(this.root);
        }

        public void DeleteMax()
        {
            this.root = DeleteMax(this.root);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            List<T> result= new List<T>();

            DfsRange(this.root, result, startRange, endRange);

            return result;
        }

        public void Delete(T element)
        {
            this.root = Delete(this.root, element);
        }

        public T Ceiling(T element)
        {
            return Select(Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return Select(Rank(element) - 1);
        }

        public void EachInOrder(Action<T> action)
        {
            DfsEachInOrder(this.root, action);
        }

        private bool IsRed(Node node)
        {
            return node == null ? false : node.Color;
        }

        private int GetCount(Node node)
        {
            return node == null ? 0 : node.Count;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }

            node.Left = DeleteMin(node.Left);

            UpdateCount(node);

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = DeleteMax(node.Right);

            UpdateCount(node);

            return node;
        }

        private void UpdateCount(Node node)
        {
            node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            var cmp = element.CompareTo(node.Value);

            if (cmp < 0)
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                    node = moveRedLeft(node);
                node.Left = Delete(node.Left, element);
            }
            else
            {
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }

                if (cmp == 0 && (node.Right == null))
                {
                    return null;
                }

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = moveRedRight(node);
                }
                if (cmp == 0)
                {
                    Node temp = node;
                    node = FindMin(temp.Right);
                    node.Right = DeleteMin(temp.Right);
                    node.Left = temp.Left;
                }
                else node.Right = Delete(node.Right, element);
            }

            if (IsRed(node.Right) &&!IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }

            node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;

            return node;
        }

        private Node moveRedLeft(Node node)
        {
            if (node.Right != null)
                FlipColors(node);
            if (IsRed(node.Right?.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        private Node moveRedRight(Node node)
        {
            if (node.Left != null)
                FlipColors(node);
            if (IsRed(node.Left?.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }


        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            node = FindMin(node.Left);

            return node;
        }

        private void DfsEachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            DfsEachInOrder(node.Left, action);
            action(node.Value);
            DfsEachInOrder(node.Right, action);
        }

        private void DfsRange(Node node, List<T> result, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            if (node.Value.CompareTo(startRange) >= 0 && node.Value.CompareTo(endRange) <= 0)
            {
                result.Add(node.Value);
                DfsRange(node.Left, result, startRange, endRange);
                DfsRange(node.Right, result, startRange, endRange);
            }
            return;
        }

        private int Rank(Node node, T element)
        {
            if (node == null)
            {
                return 0;
            }

            int comparator = element.CompareTo(node.Value);

            if (comparator < 0)
            {
                return Rank(node.Left, element);
            }

            if (comparator > 0)
            {
                return 1 + GetCount(node.Left) + Rank(node.Right, element);
            }

            return GetCount(node.Left);
        }

        private Node Select(Node node, int rank)
        {
            if (node == null)
            {
                return null;
            }

            var leftSideCount = GetCount(node.Left);

            if (leftSideCount == rank)
            {
                return node;
            }

            if (leftSideCount > rank)
            {
                return Select(node.Left, rank);
            }

            return Select(node.Right, rank - (leftSideCount + 1));
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Count = 1;
                this.Color = RED;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
            public bool Color { get; set; }
        }
    }
}