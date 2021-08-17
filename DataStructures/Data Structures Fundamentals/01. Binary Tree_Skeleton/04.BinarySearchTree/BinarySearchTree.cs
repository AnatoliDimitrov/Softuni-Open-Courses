using System.Xml.Linq;

namespace _04.BinarySearchTree
{
    using System;

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

        public bool Contains(T element)
        {
            if (!CheckRootExist())
            {
                return false;
            }
            else
            {
                var current = this.Root;

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
        }

        private int ElementCompare(T element, T nodeElement)
        {
            return element.CompareTo(nodeElement);
        }

        public void Insert(T element)
        {
            var node = new Node<T>(element, null, null);

            if (!CheckRootExist())
            {
                this.Root = node;
            }
            else
            {
                var current = this.Root;

                Node<T> last = null;

                while (current != null)
                {
                    last = current;

                    if (NodeCompare(node, current) > 0)
                    {
                        current = current.RightChild;
                    }
                    else if (NodeCompare(node, current) < 0)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if (NodeCompare(node, last) > 0)
                {
                    last.RightChild = node;
                }

                if (NodeCompare(node, last) < 0)
                {
                    last.LeftChild = node;
                }
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            BinarySearchTree<T> result = null;

            if (!CheckRootExist())
            {
                return result;
            }
            else
            {
                var current = this.Root;

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
        }

        private int NodeCompare(Node<T> node, Node<T> current)
        {
            return node.Value.CompareTo(current.Value);
        }

        private bool CheckRootExist()
        {
            return this.Root != null;
        }
    }
}
