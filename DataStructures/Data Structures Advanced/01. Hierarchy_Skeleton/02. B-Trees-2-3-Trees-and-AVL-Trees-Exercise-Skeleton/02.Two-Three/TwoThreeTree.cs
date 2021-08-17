namespace _02.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T key)
        {
            this.root = Insert(this.root, key);
        }

        private TreeNode<T> Insert(TreeNode<T> tree, T value)
        {
            if (tree == null)
            {
                return new TreeNode<T>(value);
            }

            TreeNode<T> result = null;

            if (tree.IsLeaf())
            {
                return MergeNodes(tree, new TreeNode<T>(value));
            }

            if (value.CompareTo(tree.LeftKey) < 0)
            {
                result = Insert(tree.LeftChild, value);

                if (result == tree.LeftChild)
                {
                    return tree;
                }
                else
                {
                    return MergeNodes(tree, result);
                }
            }
            else if (tree.IsTwoNode() || value.CompareTo(tree.RightKey) < 0)
            {
                result = Insert(tree.MiddleChild, value);

                if (result == tree.MiddleChild)
                {
                    return tree;
                }
                else
                {
                    return MergeNodes(tree, result);
                }
            }
            else
            {
                result = Insert(tree.RightChild, value);

                if (result == tree.RightChild)
                {
                    return tree;
                }
                else
                {
                    return MergeNodes(tree, result);
                }
            }  
        }

        private TreeNode<T> MergeNodes(TreeNode<T> oldNode, TreeNode<T> newNode)
        {
            if (oldNode.RightKey == null)
            {
                if (oldNode.LeftKey.CompareTo(newNode.LeftKey) < 0)
                {
                    oldNode.RightKey = newNode.LeftKey;
                    oldNode.MiddleChild = newNode.LeftChild;
                    oldNode.RightChild = newNode.MiddleChild;
                }
                else
                {
                    oldNode.RightKey = oldNode.LeftKey;
                    oldNode.RightChild = oldNode.MiddleChild;
                    oldNode.LeftKey = newNode.LeftKey;
                    oldNode.MiddleChild = newNode.MiddleChild;
                }

                return oldNode;
            }
            else if (oldNode.LeftKey.CompareTo(newNode.LeftKey) >= 0)
            {
                var newTree = new TreeNode<T>(oldNode.LeftKey);
                newTree.LeftChild = newNode;
                newTree.MiddleChild = oldNode;

                newNode.LeftChild = oldNode.LeftChild;
                oldNode.LeftChild = oldNode.MiddleChild;
                oldNode.MiddleChild = oldNode.RightChild;
                oldNode.RightChild = null;
                oldNode.LeftKey = oldNode.RightKey;
                oldNode.RightKey = default;
                return newTree;

            }
            else if (oldNode.RightKey.CompareTo(newNode.LeftKey) >= 0)
            {
                newNode.MiddleChild = new TreeNode<T>(oldNode.RightKey)
                {
                    LeftChild = newNode.MiddleChild,
                    MiddleChild = oldNode.RightChild
                };

                newNode.LeftChild = oldNode;
                oldNode.RightKey = default;
                oldNode.RightChild = null;
                return newNode;
            }
            else
            {
                var newTree = new TreeNode<T>(oldNode.RightKey);
                newTree.LeftChild = oldNode;
                newTree.MiddleChild = newNode;

                newNode.LeftChild = oldNode.RightChild;
                oldNode.RightChild = null;
                oldNode.RightKey = default;
                return newTree;
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
