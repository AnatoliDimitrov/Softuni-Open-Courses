namespace Tree
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.Parent = null;
            this._children = new List<Tree<T>>();

            foreach (var child in children)
            {
                child.Parent = this;
                _children.Add(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            string intender = "";

            StringBuilder result = new StringBuilder();

            return DFSString(this, intender, result);
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            Tree<T> result = null;
            int resultLevel = 0;

            if (this.Children.Count == 0)
            {
                return this;
            }
            
            DFSDeepestNode(this, ref result, ref resultLevel, -1);

            return result;
        }

        public List<T> GetLeafKeys()
        {
            Stack<T> stack = new Stack<T>();

            if (this.Children.Count == 0)
            {
                return stack.ToArray().ToList();
            }

            DFSLeafs(this, stack);

            return stack.ToArray().ToList();
        }

        public List<T> GetMiddleKeys()
        {
            List<T> list = new List<T>();

            if (this.Children.Count == 0)
            {
                return list;
            }

            DFSMiddleNodes(this, list);

            return list;
        }

        private void DFSMiddleNodes(Tree<T> tree, List<T> list)
        {
            foreach (var child in tree.Children)
            {
                if (child.Parent != null && child.Children.Count > 0)
                {
                    list.Add(child.Key);
                }
                DFSMiddleNodes(child, list);
            }
        }

        public List<T> GetLongestPath()
        {
            Stack<T> result = new Stack<T>();

            var currentNode = GetDeepestLeftomostNode();

            while (currentNode.Parent != null)
            {
                result.Push(currentNode.Key);
                currentNode = currentNode.Parent;
            }

            result.Push(currentNode.Key);

            return result.ToList();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();

            List<T> currentPath = new List<T>();

            DFSFindPaths(ref result, currentPath, this, sum);

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            List<Tree<T>> result = new List<Tree<T>>();

            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            if (IsEqual(this, sum))
            {
                result.Add(this);
            }

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var tree = queue.Dequeue();

                foreach (var child in tree.Children)
                {
                    if (IsEqual(child, sum))
                    {
                        result.Add(child);
                    }
                    queue.Enqueue(child);
                }
            }

            return result;
        }
        
        private string DFSString(Tree<T> tree, string intender, StringBuilder result)
        {

            result.AppendLine(intender + tree.Key.ToString());

            foreach (var child in tree.Children)
            {
                DFSString(child, intender + "  ", result);
            }

            return result.ToString().TrimEnd();
        }

        private void DFSLeafs(Tree<T> tree, Stack<T> stack)
        {
            foreach (var child in tree.Children)
            {
                if (child.Children.Count == 0)
                {
                    stack.Push(child.Key);
                }
                DFSLeafs(child, stack);
            }
        }

        private void DFSDeepestNode(Tree<T> tree, ref Tree<T> result, ref int resultLevel, int level)
        {
            level++;

            if (tree.Children.Count == 0 && level > resultLevel)
            {
                result = tree;
                resultLevel = level;
            }

            foreach (var child in tree.Children)
            {
                DFSDeepestNode(child, ref result, ref resultLevel, level);
            }
        }

        private void DFSFindPaths(ref List<List<T>> result, List<T> currentPath, Tree<T> tree, int sum)
        {
            currentPath.Add(tree.Key);

            foreach (var child in tree.Children)
            {
                DFSFindPaths(ref result, currentPath, child, sum);
            }

            int currentSum = currentPath
                .Select(t => int.Parse(t.ToString()))
                .Sum();

            if (currentSum == sum)
            {
                result.Add(new List<T>(currentPath));
            }

            currentPath.RemoveAt(currentPath.Count - 1);
        }

        private bool IsEqual(Tree<T> tree, int sum)
        {
            int currentSum = 0;

            DFSSubTree(tree, ref currentSum);

            return sum == currentSum;
        }

        private void DFSSubTree(Tree<T> tree, ref int currentSum)
        {
            currentSum += int.Parse(tree.Key.ToString());

            foreach (var child in tree.Children)
            {
                DFSSubTree(child, ref currentSum);
            }
        }
    }
}
