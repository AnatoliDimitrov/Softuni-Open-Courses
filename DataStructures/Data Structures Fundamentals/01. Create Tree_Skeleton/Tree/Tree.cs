namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this._children = new List<Tree<T>>();
            this.Parent = null;
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (this.Children.Count == 0) //TODO better check
            {
                return new List<T>();
            }

            queue.Enqueue(this);

            result.Add(this.Value);

            while (queue.Count != 0)
            {
                var tree = queue.Dequeue();

                foreach (var child in tree.Children)
                {
                    queue.Enqueue(child);
                    result.Add(child.Value);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var traversed = new List<T>();

            if (this.Children.Count == 0)
            {
                return new List<T>();
            }

            DFS(this, traversed);

            return traversed;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parent = FindBFS(parentKey);

            ParentIsNULL(parent);

            child.Parent = parent;
            parent._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> node = FindBFS(nodeKey);

            ParentIsNULL(node);

            if (node.Parent == null)
            {
                this.Value = default;
                this._children.Clear();
            }
            else
            {
                var parent = node.Parent;
                node._children.Clear();
                parent._children.Remove(node);
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = FindBFS(firstKey);
            var secondNode = FindBFS(secondKey);

            ParentIsNULL(firstNode);
            ParentIsNULL(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                firstNode.Value = secondNode.Value;
                firstNode._children.Clear();

                foreach (var item in secondNode._children)
                {
                    firstNode._children.Add(item);
                }

                secondNode.Value = default;
                secondNode._children.Clear();
                return;
            }

            if (secondParent == null)
            {
                secondNode.Value = firstNode.Value;
                secondNode._children.Clear();

                foreach (var item in firstNode._children)
                {
                    secondNode._children.Add(item);
                }

                firstNode.Value = default;
                firstNode._children.Clear();
                return;
            }

            var indexOfFirst = firstParent._children.IndexOf(firstNode);
            var indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }


        private void DFS(Tree<T> tree, List<T> traversed)
        {
            foreach (var child in tree.Children)
            {
                DFS(child, traversed);
                traversed.Add(child.Value);
            }

            if (tree.Parent == null)
            {
                traversed.Add(tree.Value);
            }
        }

        private static void ParentIsNULL(Tree<T> parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException();
            }
        }

        private Tree<T> FindBFS(T parentKey)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            Tree<T> parent = null;

            queue.Enqueue(this);

            if (this.Value.Equals(parentKey))
            {
                return this;
            }

            while (queue.Count != 0)
            {
                var tree = queue.Dequeue();

                foreach (var item in tree.Children)
                {
                    if (item.Value.Equals(parentKey))
                    {
                        parent = item;
                        queue = new Queue<Tree<T>>();
                        break;
                    }
                    queue.Enqueue(item);
                }
            }

            return parent;
        }
    }
}
