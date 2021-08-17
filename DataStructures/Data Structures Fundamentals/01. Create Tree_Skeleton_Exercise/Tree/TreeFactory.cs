namespace Tree
{
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;
        private Tree<int> root;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string[] line = input[i].Split(' ');
                int key = int.Parse(line[0]);
                int child = int.Parse(line[1]);
                if (nodesBykeys.Count == 0)
                {
                    var childTree = CreateNodeByKey(child);
                    var parentTree = CreateNodeByKey(key);

                    this.root = parentTree;

                    childTree.AddParent(parentTree);
                    parentTree.AddChild(childTree);

                    this.nodesBykeys[key] = parentTree;
                }
                else
                {
                    AddEdge(key, child);
                }
            }

            return GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            return new Tree<int>(key);
        }

        public void AddEdge(int parent, int child)
        {
            var childTree = new Tree<int>(child);
            Tree<int> parentTree = null;

            Queue<Tree<int>> queue = new Queue<Tree<int>>();

            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();

                if (this.root.Key == parent)
                {
                    root.AddChild(childTree);
                    childTree.AddParent(root);
                    return;
                }

                foreach (var item in tree.Children)
                {
                    queue.Enqueue(item);
                    if (item.Key == parent)
                    {
                        parentTree = item;
                        parentTree.AddChild(childTree);
                        childTree.AddParent(parentTree);
                        queue = new Queue<Tree<int>>();
                        break;
                    }
                }
            }

            childTree.AddParent(parentTree);
        }

        private Tree<int> GetRoot()
        {

            Tree<int> result = null;

            foreach (var tree in nodesBykeys)
            {
                if (tree.Value.Parent == null)
                {
                    result = tree.Value;
                    break;
                }
            }

            return result;
        }
    }
}
