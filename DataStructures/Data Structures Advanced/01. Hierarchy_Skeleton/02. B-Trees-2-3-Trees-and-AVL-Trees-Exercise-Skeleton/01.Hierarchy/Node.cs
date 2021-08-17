using System.Collections.Generic;

namespace _01.Hierarchy
{
    public class Node<T>
    {

        public Node(T element, Node<T> parent = null)
        {
            this.Value = element;
            this.Parent = parent;
            this.Children = new List<Node<T>>();
        }

        public T Value { get; private set; }

        public List<Node<T>> Children { get; private set; }

        public Node<T> Parent { get;  set; }
    }
}
