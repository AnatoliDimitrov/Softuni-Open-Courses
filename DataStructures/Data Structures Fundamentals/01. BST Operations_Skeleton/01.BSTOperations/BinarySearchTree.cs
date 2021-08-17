namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {

        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.LeftChild = root.LeftChild;
            this.RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => GetCount(this.Root, 0);

        public bool Contains(T element)
        {
            var current = this.Root;

            if (CheckRootForNull(current))
            {
                return false;
            }

            while (current != null)
            {
                if (ElementCompare(element, current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else if (ElementCompare(element, current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            Node<T> node = new Node<T>(element, null, null);

            if (CheckRootForNull(this.Root))
            {
                this.Root = node;
                return;
            }

            Node<T> current = this.Root;

            Node<T> last = null;

            while (current != null)
            {
                last = current;

                if (ElementCompare(node.Value, current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else if (ElementCompare(node.Value, current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    return;
                }
            }

            if (ElementCompare(node.Value, last.Value) > 0)
            {
                last.RightChild = node;
            }
            else if (ElementCompare(node.Value, last.Value) < 0)
            {
                last.LeftChild = node;
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            IAbstractBinarySearchTree<T> result = null;

            var current = this.Root;

            if (CheckRootForNull(current))
            {
                return result;
            }

            while (current != null)
            {
                if (ElementCompare(element, current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else if (ElementCompare(element, current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else
                {
                    result = new BinarySearchTree<T>(current);
                    break;
                }
            }

            return result;
        }

        public void EachInOrder(Action<T> action)
        {
            var node = this.Root;

            if (node == null)
            {
                return;
            }
            else
            {
                TraverseInOrder(node, action);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();

            if (CheckRootForNull(this.Root))
            {
                return result;
            }

            DFSRange(ref result, this.Root, lower, upper);

            return result;
        }

        public void DeleteMin()
        {
            var current = this.Root;
            Node<T> prev = null;

            if (CheckRootForNull(current))
            {
                throw new InvalidOperationException("No Elements!");
            }

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    prev = current;

                    current = current.LeftChild;
                }

                prev.LeftChild = current.RightChild;
            }
        }

        public void DeleteMax()
        {
            var current = this.Root;
            Node<T> prev = null;

            if (CheckRootForNull(current))
            {
                throw new InvalidOperationException("No Elements!");
            }

            if(this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    prev = current;

                    current = current.RightChild;
                }

                prev.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            if (CheckRootForNull(this.Root))
            {
                return 0;
            }

            int count = DfsRank(this.Root, 0, element);

            return count;
        }

        private int DfsRank(Node<T> node, int count, T element)
        {
            if (node.Value.CompareTo(element) > 0)
            {
                return count++;
            }
            else
            {
                count = GetCount(node.LeftChild, count);
                count = GetCount(node.RightChild, count);
            }

            return count -1;
        }

        private int GetCount(Node<T> node, int count)
        {
            if (CheckRootForNull(node))
            {
                return count;
            }
            else
            {
                count++;
                count = GetCount(node.LeftChild, count);
                count = GetCount(node.RightChild, count);
            }

            return count;
        }

        private bool CheckRootForNull(Node<T> root)
        {
            return root == null;
        }

        private int ElementCompare(T nodeValue, T currentValue)
        {
            return nodeValue.CompareTo(currentValue);
        }

        private void TraverseInOrder(Node<T> node, Action<T> action)
        {
            if (!CheckRootForNull(node.LeftChild))
            {
                TraverseInOrder(node.LeftChild, action);
            }

            action.Invoke(node.Value);

            if (!CheckRootForNull(node.RightChild))
            {
                TraverseInOrder(node.RightChild, action);
            }
        }

        private void DFSRange(ref List<T> result, Node<T> node, T lower, T upper)
        {
            if (CheckRootForNull(node))
            {
                return;
            }

            if (InBounderies(node.Value, lower, upper))
            {
                result.Add(node.Value);
            }

            DFSRange(ref result, node.LeftChild, lower, upper);
            DFSRange(ref result, node.RightChild, lower, upper);

        }

        private bool InBounderies(T nodeValue, T lower, T upper)
        {
            return nodeValue.CompareTo(lower) > 0 ||
                   nodeValue.CompareTo(lower) == 0 &&
                   nodeValue.CompareTo(upper) < 0 ||
                   nodeValue.CompareTo(upper) == 0;
        }
    }
}
