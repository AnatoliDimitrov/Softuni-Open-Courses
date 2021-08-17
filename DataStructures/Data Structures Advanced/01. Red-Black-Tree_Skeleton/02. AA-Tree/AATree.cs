using System.Net.Security;

namespace _02._AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public int CountNodes()
        {
            return GetCount(this.root);
        }

        private int GetCount(Node<T> node)
        {
            return node == null ? 0 : node.Count;
        }

        public bool IsEmpty()
        {
            return this.root == null;
        }

        public void Clear()
        {
            this.root = null;
        }

        public void Insert(T element)
        {
            this.root = Insert(this.root, element);
        }


        public bool Search(T element)
        {
            var current = this.root;

            while (current != null)
            {
                var comparator = current.Element.CompareTo(element);

                if (comparator > 0)
                {
                    current = current.Left;
                } 
                else if (comparator < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void InOrder(Action<T> action)
        {
            InOrder(this.root, action);
        }

        public void PreOrder(Action<T> action)
        {
            PreOrder(this.root, action);
        }

        public void PostOrder(Action<T> action)
        {
            PostOrder(this.root, action);
        }

        private void PostOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            PostOrder(node.Left, action);
            PostOrder(node.Right, action);
            action.Invoke(node.Element);
        }

        //private methods (helpers)

        private Node<T> Insert(Node<T> node, T element)
        {
            if (node == null)
            {
                return new Node<T>(element);
            }

            int comparator = element.CompareTo(node.Element);

            if (comparator < 0)
            {
                node.Left = Insert(node.Left, element);
            }

            if (comparator > 0)
            {
                node.Right = Insert(node.Right, element);
            }

            node = Skew(node);
            node = Split(node);

            node.Count = UpdateCount(node);

            return node;
        }

        private int UpdateCount(Node<T> node)
        {
            return GetCount(node.Left) + GetCount(node.Right) + 1;
        }

        private Node<T> Split(Node<T> node)
        {
            if ((node.Right != null && node.Right.Right != null) && node.Level == node.Right.Level && node.Right.Right.Level == node.Level)
            {
                var temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;
                node.Count = UpdateCount(node);
                temp.Level = Level(temp.Right) + 1;
                return temp;
            }

            return node;
        }

        private int Level(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Level;
        }

        private Node<T> Skew(Node<T> node)
        {
            if (node.Level == node.Left?.Level)
            {
                var temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;

                node.Count = UpdateCount(node);
                return temp;
            }
            else
            {
                return node;
            }
        }

        private void InOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.Left, action);
            action.Invoke(node.Element);
            InOrder(node.Right, action);
        }

        private void PreOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            action.Invoke(node.Element);
            PreOrder(node.Left, action);
            PreOrder(node.Right, action);
        }
    }
}