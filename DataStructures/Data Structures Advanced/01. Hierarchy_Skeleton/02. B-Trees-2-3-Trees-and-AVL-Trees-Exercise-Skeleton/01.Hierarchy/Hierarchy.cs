using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private readonly Node<T> _root;
        private readonly Dictionary<T, Node<T>> _nodes;

        public Hierarchy(T element)
        {
            this._root = new Node<T>(element);
            this._nodes = new Dictionary<T, Node<T>> {{element, _root}};
        }

        public int Count => this._nodes.Count;

        public void Add(T element, T child)
        {
            if (!this._nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this._nodes.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var childNode = new Node<T>(child, this._nodes[element]);

            this._nodes.Add(child, childNode);

            this._nodes[element].Children.Add(childNode);
        }

        public void Remove(T element)
        {
            if (!this._nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (element.Equals(this._root.Value))
            {
                throw new InvalidOperationException();
            }

            var node = _nodes[element];
            var parent = node.Parent;

            foreach (var child in node.Children)
            {
                parent.Children.Add(child);
                child.Parent = parent;
            }

            parent.Children.Remove(node);
            _nodes.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!this._nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            List<T> result = new List<T>();

            foreach (var child in this._nodes[element].Children)
            {
                    result.Add(child.Value);
            }

            return result;
        }

        public T GetParent(T element)
        {
            if (!this._nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var node = this._nodes[element];

            if (node.Parent == null)
            {
                return default;
            }

            return node.Parent.Value;
        }

        public bool Contains(T element)
        {
            return this._nodes.ContainsKey(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var result = new List<T>();

            foreach (var el in this._nodes)
            {
                if (other.Contains(el.Key))
                {
                    result.Add(el.Key);
                }
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();

            queue.Enqueue(this._root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                yield return current.Value;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}