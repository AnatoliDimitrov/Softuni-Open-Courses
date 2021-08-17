// Decompiled with JetBrains decompiler
// Type: SelfBalancedTree.AVLTree`1
// Assembly: AvlTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 89C0474A-53AE-4987-A141-C9026884FE6E
// Assembly location: C:\Users\apdim\.nuget\packages\avltree\1.0.0\lib\net20\AvlTree.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace SelfBalancedTree
{
    public class AVLTree<T>
    {
        private AVLTree<T>.Node<T> Root;
        private IComparer<T> comparer;

        public AVLTree() => this.comparer = AVLTree<T>.GetComparer();

        public AVLTree(IEnumerable<T> elems, IComparer<T> comparer)
        {
            this.comparer = comparer;
            if (elems == null)
                return;
            foreach (T elem in elems)
                this.Add(elem);
        }

        public AVLTree<T>.Node<T> root
        {
            get { return this.Root; }
        }

        public IEnumerable<T> ValuesCollection
        {
            get
            {
                if (this.Root != null)
                {
                    for (AVLTree<T>.Node<T> p = AVLTree<T>.FindMin(this.Root); p != null; p = AVLTree<T>.Successor(p))
                        yield return p.Data;
                }
            }
        }

        public IEnumerable<T> ValuesCollectionDescending
        {
            get
            {
                if (this.Root != null)
                {
                    for (AVLTree<T>.Node<T> p = AVLTree<T>.FindMax(this.Root); p != null; p = AVLTree<T>.Predecesor(p))
                        yield return p.Data;
                }
            }
        }

        public bool Add(T arg)
        {
            bool wasAdded = false;
            bool wasSuccessful = false;
            this.Root = this.Add(this.Root, arg, ref wasAdded, ref wasSuccessful);
            return wasSuccessful;
        }

        public bool Delete(T arg)
        {
            bool wasSuccessful = false;
            if (this.Root != null)
            {
                bool wasDeleted = false;
                this.Root = this.Delete(this.Root, arg, ref wasDeleted, ref wasSuccessful);
            }
            return wasSuccessful;
        }

        public bool GetMin(out T value)
        {
            if (this.Root != null)
            {
                AVLTree<T>.Node<T> min = AVLTree<T>.FindMin(this.Root);
                if (min != null)
                {
                    value = min.Data;
                    return true;
                }
            }
            value = default(T);
            return false;
        }

        public bool GetMax(out T value)
        {
            if (this.Root != null)
            {
                AVLTree<T>.Node<T> max = AVLTree<T>.FindMax(this.Root);
                if (max != null)
                {
                    value = max.Data;
                    return true;
                }
            }
            value = default(T);
            return false;
        }

        public bool Contains(T arg) => this.Search(this.Root, arg) != null;

        public bool DeleteMin()
        {
            if (this.Root == null)
                return false;
            bool wasDeleted = false;
            bool wasSuccessful = false;
            this.Root = this.DeleteMin(this.Root, ref wasDeleted, ref wasSuccessful);
            return wasSuccessful;
        }

        public bool DeleteMax()
        {
            if (this.Root == null)
                return false;
            bool wasDeleted = false;
            bool wasSuccessful = false;
            this.Root = this.DeleteMax(this.Root, ref wasDeleted, ref wasSuccessful);
            return wasSuccessful;
        }

        public AVLTree<T> Concat(AVLTree<T> other)
        {
            if (other == null)
                return this;
            AVLTree<T>.Node<T> node = this.Concat(this.Root, other.Root);
            if (node == null)
                return (AVLTree<T>)null;
            return new AVLTree<T>() { Root = node };
        }

        public bool Split(
          T value,
          AVLTree<T>.SplitOperationMode mode,
          out AVLTree<T> splitLeftTree,
          out AVLTree<T> splitRightTree)
        {
            splitLeftTree = (AVLTree<T>)null;
            splitRightTree = (AVLTree<T>)null;
            AVLTree<T>.Node<T> splitLeftTree1 = (AVLTree<T>.Node<T>)null;
            AVLTree<T>.Node<T> splitRightTree1 = (AVLTree<T>.Node<T>)null;
            bool wasFound = false;
            this.Split(this.Root, value, ref splitLeftTree1, ref splitRightTree1, mode, ref wasFound);
            if (wasFound)
            {
                splitLeftTree = new AVLTree<T>()
                {
                    Root = splitLeftTree1
                };
                splitRightTree = new AVLTree<T>()
                {
                    Root = splitRightTree1
                };
            }
            return wasFound;
        }

        public int GetHeightLogN() => this.GetHeightLogN(this.Root);

        public void Clear() => this.Root = (AVLTree<T>.Node<T>)null;

        public void Print() => this.Visit((AVLTree<T>.VisitNodeHandler<AVLTree<T>.Node<T>>)((node, level) =>
        {
            Console.Write(new string(' ', 2 * level));
            Console.WriteLine("{0, 6}", (object)node.Data);
        }));

        public int GetCount()
        {
            int count = 0;
            this.Visit((AVLTree<T>.VisitNodeHandler<AVLTree<T>.Node<T>>)((node, level) => ++count));
            return count;
        }

        private static IComparer<T> GetComparer()
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || typeof(IComparable).IsAssignableFrom(typeof(T)))
                return (IComparer<T>)Comparer<T>.Default;
            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The type {0} cannot be compared. It must implement IComparable<T> or IComparable interface", (object)typeof(T).FullName));
        }

        private int GetHeightLogN(AVLTree<T>.Node<T> node)
        {
            if (node == null)
                return 0;
            int heightLogN = this.GetHeightLogN(node.Left);
            if (node.Balance == 1)
                ++heightLogN;
            return 1 + heightLogN;
        }

        private AVLTree<T>.Node<T> Add(
          AVLTree<T>.Node<T> elem,
          T data,
          ref bool wasAdded,
          ref bool wasSuccessful)
        {
            if (elem == null)
            {
                elem = new AVLTree<T>.Node<T>()
                {
                    Data = data,
                    Left = (AVLTree<T>.Node<T>)null,
                    Right = (AVLTree<T>.Node<T>)null,
                    Balance = 0
                };
                elem.Height = 1;
                wasAdded = true;
                wasSuccessful = true;
            }
            else
            {
                int num = this.comparer.Compare(data, elem.Data);
                if (num < 0)
                {
                    AVLTree<T>.Node<T> node = this.Add(elem.Left, data, ref wasAdded, ref wasSuccessful);
                    if (elem.Left != node)
                    {
                        elem.Left = node;
                        node.Parent = elem;
                    }
                    if (wasAdded)
                    {
                        --elem.Balance;
                        if (elem.Balance == 0)
                            wasAdded = false;
                        else if (elem.Balance == -2)
                        {
                            switch (node.Balance)
                            {
                                case -1:
                                    elem = AVLTree<T>.RotateRight(elem);
                                    elem.Balance = 0;
                                    elem.Right.Balance = 0;
                                    break;
                                case 1:
                                    int balance = node.Right.Balance;
                                    elem.Left = AVLTree<T>.RotateLeft(node);
                                    elem = AVLTree<T>.RotateRight(elem);
                                    elem.Balance = 0;
                                    elem.Left.Balance = balance == 1 ? -1 : 0;
                                    elem.Right.Balance = balance == -1 ? 1 : 0;
                                    break;
                            }
                            wasAdded = false;
                        }
                    }
                }
                else if (num > 0)
                {
                    AVLTree<T>.Node<T> node = this.Add(elem.Right, data, ref wasAdded, ref wasSuccessful);
                    if (elem.Right != node)
                    {
                        elem.Right = node;
                        node.Parent = elem;
                    }
                    if (wasAdded)
                    {
                        ++elem.Balance;
                        if (elem.Balance == 0)
                            wasAdded = false;
                        else if (elem.Balance == 2)
                        {
                            switch (node.Balance)
                            {
                                case -1:
                                    int balance = node.Left.Balance;
                                    elem.Right = AVLTree<T>.RotateRight(node);
                                    elem = AVLTree<T>.RotateLeft(elem);
                                    elem.Balance = 0;
                                    elem.Left.Balance = balance == 1 ? -1 : 0;
                                    elem.Right.Balance = balance == -1 ? 1 : 0;
                                    break;
                                case 1:
                                    elem = AVLTree<T>.RotateLeft(elem);
                                    elem.Balance = 0;
                                    elem.Left.Balance = 0;
                                    break;
                            }
                            wasAdded = false;
                        }
                    }
                }
                elem.Height = 1 + Math.Max(elem.Left != null ? elem.Left.Height : 0, elem.Right != null ? elem.Right.Height : 0);
            }
            return elem;
        }

        private AVLTree<T>.Node<T> Delete(
          AVLTree<T>.Node<T> node,
          T arg,
          ref bool wasDeleted,
          ref bool wasSuccessful)
        {
            int num = this.comparer.Compare(arg, node.Data);
            if (num < 0)
            {
                if (node.Left != null)
                {
                    AVLTree<T>.Node<T> node1 = this.Delete(node.Left, arg, ref wasDeleted, ref wasSuccessful);
                    if (node.Left != node1)
                        node.Left = node1;
                    if (wasDeleted)
                        ++node.Balance;
                }
            }
            else if (num == 0)
            {
                wasDeleted = true;
                if (node.Left != null && node.Right != null)
                {
                    AVLTree<T>.Node<T> min = AVLTree<T>.FindMin(node.Right);
                    T data = node.Data;
                    node.Data = min.Data;
                    min.Data = data;
                    wasDeleted = false;
                    AVLTree<T>.Node<T> node1 = this.Delete(node.Right, data, ref wasDeleted, ref wasSuccessful);
                    if (node.Right != node1)
                        node.Right = node1;
                    if (wasDeleted)
                        --node.Balance;
                }
                else
                {
                    if (node.Left == null)
                    {
                        wasSuccessful = true;
                        if (node.Right != null)
                            node.Right.Parent = node.Parent;
                        return node.Right;
                    }
                    wasSuccessful = true;
                    if (node.Left != null)
                        node.Left.Parent = node.Parent;
                    return node.Left;
                }
            }
            else if (node.Right != null)
            {
                AVLTree<T>.Node<T> node1 = this.Delete(node.Right, arg, ref wasDeleted, ref wasSuccessful);
                if (node.Right != node1)
                    node.Right = node1;
                if (wasDeleted)
                    --node.Balance;
            }
            if (wasDeleted)
            {
                if (node.Balance == 1 || node.Balance == -1)
                    wasDeleted = false;
                else if (node.Balance == -2)
                {
                    AVLTree<T>.Node<T> left = node.Left;
                    switch (left.Balance)
                    {
                        case -1:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Right.Balance = 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 1;
                            node.Right.Balance = -1;
                            wasDeleted = false;
                            break;
                        case 1:
                            int balance = left.Right.Balance;
                            node.Left = AVLTree<T>.RotateLeft(left);
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Left.Balance = balance == 1 ? -1 : 0;
                            node.Right.Balance = balance == -1 ? 1 : 0;
                            break;
                    }
                }
                else if (node.Balance == 2)
                {
                    AVLTree<T>.Node<T> right = node.Right;
                    switch (right.Balance)
                    {
                        case -1:
                            int balance = right.Left.Balance;
                            node.Right = AVLTree<T>.RotateRight(right);
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = balance == 1 ? -1 : 0;
                            node.Right.Balance = balance == -1 ? 1 : 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = -1;
                            node.Left.Balance = 1;
                            wasDeleted = false;
                            break;
                        case 1:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = 0;
                            break;
                    }
                }
                node.Height = 1 + Math.Max(node.Left != null ? node.Left.Height : 0, node.Right != null ? node.Right.Height : 0);
            }
            return node;
        }

        private static AVLTree<T>.Node<T> FindMin(AVLTree<T>.Node<T> node)
        {
            while (node != null && node.Left != null)
                node = node.Left;
            return node;
        }

        private static AVLTree<T>.Node<T> FindMax(AVLTree<T>.Node<T> node)
        {
            while (node != null && node.Right != null)
                node = node.Right;
            return node;
        }

        private AVLTree<T>.Node<T> Search(AVLTree<T>.Node<T> subtree, T data)
        {
            if (subtree == null)
                return (AVLTree<T>.Node<T>)null;
            if (this.comparer.Compare(data, subtree.Data) < 0)
                return this.Search(subtree.Left, data);
            return this.comparer.Compare(data, subtree.Data) > 0 ? this.Search(subtree.Right, data) : subtree;
        }

        private AVLTree<T>.Node<T> DeleteMin(
          AVLTree<T>.Node<T> node,
          ref bool wasDeleted,
          ref bool wasSuccessful)
        {
            if (node.Left == null)
            {
                wasDeleted = true;
                wasSuccessful = true;
                if (node.Right != null)
                    node.Right.Parent = node.Parent;
                return node.Right;
            }
            node.Left = this.DeleteMin(node.Left, ref wasDeleted, ref wasSuccessful);
            if (wasDeleted)
                ++node.Balance;
            if (wasDeleted)
            {
                if (node.Balance == 1 || node.Balance == -1)
                    wasDeleted = false;
                else if (node.Balance == -2)
                {
                    switch (node.Left.Balance)
                    {
                        case -1:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Right.Balance = 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 1;
                            node.Right.Balance = -1;
                            wasDeleted = false;
                            break;
                        case 1:
                            int balance1 = node.Left.Right.Balance;
                            node.Left = AVLTree<T>.RotateLeft(node.Left);
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Left.Balance = balance1 == 1 ? -1 : 0;
                            node.Right.Balance = balance1 == -1 ? 1 : 0;
                            break;
                    }
                }
                else if (node.Balance == 2)
                {
                    switch (node.Right.Balance)
                    {
                        case -1:
                            int balance2 = node.Right.Left.Balance;
                            node.Right = AVLTree<T>.RotateRight(node.Right);
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = balance2 == 1 ? -1 : 0;
                            node.Right.Balance = balance2 == -1 ? 1 : 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = -1;
                            node.Left.Balance = 1;
                            wasDeleted = false;
                            break;
                        case 1:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = 0;
                            break;
                    }
                }
                node.Height = 1 + Math.Max(node.Left != null ? node.Left.Height : 0, node.Right != null ? node.Right.Height : 0);
            }
            return node;
        }

        private AVLTree<T>.Node<T> DeleteMax(
          AVLTree<T>.Node<T> node,
          ref bool wasDeleted,
          ref bool wasSuccessful)
        {
            if (node.Right == null)
            {
                wasDeleted = true;
                wasSuccessful = true;
                if (node.Left != null)
                    node.Left.Parent = node.Parent;
                return node.Left;
            }
            node.Right = this.DeleteMax(node.Right, ref wasDeleted, ref wasSuccessful);
            if (wasDeleted)
                --node.Balance;
            if (wasDeleted)
            {
                if (node.Balance == 1 || node.Balance == -1)
                    wasDeleted = false;
                else if (node.Balance == -2)
                {
                    switch (node.Left.Balance)
                    {
                        case -1:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Right.Balance = 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 1;
                            node.Right.Balance = -1;
                            wasDeleted = false;
                            break;
                        case 1:
                            int balance1 = node.Left.Right.Balance;
                            node.Left = AVLTree<T>.RotateLeft(node.Left);
                            node = AVLTree<T>.RotateRight(node);
                            node.Balance = 0;
                            node.Left.Balance = balance1 == 1 ? -1 : 0;
                            node.Right.Balance = balance1 == -1 ? 1 : 0;
                            break;
                    }
                }
                else if (node.Balance == 2)
                {
                    switch (node.Right.Balance)
                    {
                        case -1:
                            int balance2 = node.Right.Left.Balance;
                            node.Right = AVLTree<T>.RotateRight(node.Right);
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = balance2 == 1 ? -1 : 0;
                            node.Right.Balance = balance2 == -1 ? 1 : 0;
                            break;
                        case 0:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = -1;
                            node.Left.Balance = 1;
                            wasDeleted = false;
                            break;
                        case 1:
                            node = AVLTree<T>.RotateLeft(node);
                            node.Balance = 0;
                            node.Left.Balance = 0;
                            break;
                    }
                }
                node.Height = 1 + Math.Max(node.Left != null ? node.Left.Height : 0, node.Right != null ? node.Right.Height : 0);
            }
            return node;
        }

        private static AVLTree<T>.Node<T> Predecesor(AVLTree<T>.Node<T> node)
        {
            if (node.Left != null)
                return AVLTree<T>.FindMax(node.Left);
            AVLTree<T>.Node<T> node1 = node;
            while (node1.Parent != null && node1.Parent.Left == node1)
                node1 = node1.Parent;
            return node1.Parent;
        }

        private static AVLTree<T>.Node<T> Successor(AVLTree<T>.Node<T> node)
        {
            if (node.Right != null)
                return AVLTree<T>.FindMin(node.Right);
            AVLTree<T>.Node<T> node1 = node;
            while (node1.Parent != null && node1.Parent.Right == node1)
                node1 = node1.Parent;
            return node1.Parent;
        }

        private void Visit(
          AVLTree<T>.VisitNodeHandler<AVLTree<T>.Node<T>> visitor)
        {
            if (this.Root == null)
                return;
            this.Root.Visit(visitor, 0);
        }

        private static AVLTree<T>.Node<T> RotateLeft(AVLTree<T>.Node<T> node)
        {
            AVLTree<T>.Node<T> right = node.Right;
            AVLTree<T>.Node<T> left1 = node.Left;
            AVLTree<T>.Node<T> left2 = right.Left;
            node.Right = left2;
            node.Height = 1 + Math.Max(left1 != null ? left1.Height : 0, left2 != null ? left2.Height : 0);
            AVLTree<T>.Node<T> parent = node.Parent;
            if (left2 != null)
                left2.Parent = node;
            right.Left = node;
            right.Height = 1 + Math.Max(node.Height, right.Right != null ? right.Right.Height : 0);
            node.Parent = right;
            if (parent != null)
            {
                if (parent.Left == node)
                    parent.Left = right;
                else
                    parent.Right = right;
            }
            right.Parent = parent;
            return right;
        }

        private static AVLTree<T>.Node<T> RotateRight(AVLTree<T>.Node<T> node)
        {
            AVLTree<T>.Node<T> left = node.Left;
            AVLTree<T>.Node<T> right = left.Right;
            node.Left = right;
            node.Height = 1 + Math.Max(right != null ? right.Height : 0, node.Right != null ? node.Right.Height : 0);
            AVLTree<T>.Node<T> parent = node.Parent;
            if (right != null)
                right.Parent = node;
            left.Right = node;
            left.Height = 1 + Math.Max(left.Left != null ? left.Left.Height : 0, node.Height);
            node.Parent = left;
            if (parent != null)
            {
                if (parent.Left == node)
                    parent.Left = left;
                else
                    parent.Right = left;
            }
            left.Parent = parent;
            return left;
        }

        private AVLTree<T>.Node<T> Concat(AVLTree<T>.Node<T> node1, AVLTree<T>.Node<T> node2)
        {
            if (node1 == null)
                return node2;
            if (node2 == null)
                return node1;
            bool wasAdded = false;
            bool wasDeleted = false;
            bool wasSuccessful = false;
            int height1 = node1.Height;
            int height2 = node2.Height;
            if (height1 == height2)
            {
                AVLTree<T>.Node<T> node = new AVLTree<T>.Node<T>()
                {
                    Data = default(T),
                    Left = node1,
                    Right = node2,
                    Balance = 0,
                    Height = 1 + height1
                };
                node1.Parent = node;
                node2.Parent = node;
                return this.Delete(node, default(T), ref wasDeleted, ref wasSuccessful);
            }
            if (height1 > height2)
            {
                AVLTree<T>.Node<T> min = AVLTree<T>.FindMin(node2);
                node2 = this.DeleteMin(node2, ref wasDeleted, ref wasSuccessful);
                node1 = node2 == null ? this.Add(node1, min.Data, ref wasAdded, ref wasSuccessful) : this.ConcatImpl(node1, node2, min.Data, ref wasAdded);
                return node1;
            }
            AVLTree<T>.Node<T> max = AVLTree<T>.FindMax(node1);
            node1 = this.DeleteMax(node1, ref wasDeleted, ref wasSuccessful);
            node2 = node1 == null ? this.Add(node2, max.Data, ref wasAdded, ref wasSuccessful) : this.ConcatImpl(node2, node1, max.Data, ref wasAdded);
            return node2;
        }

        private AVLTree<T>.Node<T> ConcatImpl(
          AVLTree<T>.Node<T> elem,
          AVLTree<T>.Node<T> elem2add,
          T newData,
          ref bool wasAdded)
        {
            int num1 = elem.Height - elem2add.Height;
            if (elem == null)
            {
                if (num1 > 0)
                    throw new ArgumentException("invalid input");
            }
            else
            {
                int num2 = this.comparer.Compare(elem.Data, newData);
                if (num2 < 0)
                {
                    switch (num1)
                    {
                        case 0:
                            int num3 = elem2add.Height - elem.Height;
                            elem = new AVLTree<T>.Node<T>()
                            {
                                Data = newData,
                                Left = elem,
                                Right = elem2add,
                                Balance = num3
                            };
                            wasAdded = true;
                            elem.Left.Parent = elem;
                            elem2add.Parent = elem;
                            goto label_31;
                        case 1:
                            if (elem.Balance != -1)
                                break;
                            goto case 0;
                    }
                    elem.Right = this.ConcatImpl(elem.Right, elem2add, newData, ref wasAdded);
                    if (wasAdded)
                    {
                        ++elem.Balance;
                        if (elem.Balance == 0)
                            wasAdded = false;
                    }
                    elem.Right.Parent = elem;
                    if (elem.Balance == 2)
                    {
                        if (elem.Right.Balance == -1)
                        {
                            int balance = elem.Right.Left.Balance;
                            elem.Right = AVLTree<T>.RotateRight(elem.Right);
                            elem = AVLTree<T>.RotateLeft(elem);
                            elem.Balance = 0;
                            elem.Left.Balance = balance == 1 ? -1 : 0;
                            elem.Right.Balance = balance == -1 ? 1 : 0;
                            wasAdded = false;
                        }
                        else if (elem.Right.Balance == 1)
                        {
                            elem = AVLTree<T>.RotateLeft(elem);
                            elem.Balance = 0;
                            elem.Left.Balance = 0;
                            wasAdded = false;
                        }
                        else if (elem.Right.Balance == 0)
                        {
                            elem = AVLTree<T>.RotateLeft(elem);
                            elem.Balance = -1;
                            elem.Left.Balance = 1;
                            wasAdded = true;
                        }
                    }
                }
                else if (num2 > 0)
                {
                    switch (num1)
                    {
                        case 0:
                            int num3 = elem.Height - elem2add.Height;
                            elem = new AVLTree<T>.Node<T>()
                            {
                                Data = newData,
                                Left = elem2add,
                                Right = elem,
                                Balance = num3
                            };
                            wasAdded = true;
                            elem.Right.Parent = elem;
                            elem2add.Parent = elem;
                            goto label_31;
                        case 1:
                            if (elem.Balance != 1)
                                break;
                            goto case 0;
                    }
                    elem.Left = this.ConcatImpl(elem.Left, elem2add, newData, ref wasAdded);
                    if (wasAdded)
                    {
                        --elem.Balance;
                        if (elem.Balance == 0)
                            wasAdded = false;
                    }
                    elem.Left.Parent = elem;
                    if (elem.Balance == -2)
                    {
                        if (elem.Left.Balance == 1)
                        {
                            int balance = elem.Left.Right.Balance;
                            elem.Left = AVLTree<T>.RotateLeft(elem.Left);
                            elem = AVLTree<T>.RotateRight(elem);
                            elem.Balance = 0;
                            elem.Left.Balance = balance == 1 ? -1 : 0;
                            elem.Right.Balance = balance == -1 ? 1 : 0;
                            wasAdded = false;
                        }
                        else if (elem.Left.Balance == -1)
                        {
                            elem = AVLTree<T>.RotateRight(elem);
                            elem.Balance = 0;
                            elem.Right.Balance = 0;
                            wasAdded = false;
                        }
                        else if (elem.Left.Balance == 0)
                        {
                            elem = AVLTree<T>.RotateRight(elem);
                            elem.Balance = 1;
                            elem.Right.Balance = -1;
                            wasAdded = true;
                        }
                    }
                }
                label_31:
                elem.Height = 1 + Math.Max(elem.Left != null ? elem.Left.Height : 0, elem.Right != null ? elem.Right.Height : 0);
            }
            return elem;
        }

        private AVLTree<T>.Node<T> ConcatAtJunctionPoint(
          AVLTree<T>.Node<T> node1,
          AVLTree<T>.Node<T> node2,
          T value)
        {
            bool wasAdded = false;
            bool wasSuccessful = false;
            if (node1 == null)
            {
                if (node2 != null)
                    node2 = this.Add(node2, value, ref wasAdded, ref wasSuccessful);
                else
                    node2 = new AVLTree<T>.Node<T>()
                    {
                        Data = value,
                        Balance = 0,
                        Left = (AVLTree<T>.Node<T>)null,
                        Right = (AVLTree<T>.Node<T>)null,
                        Height = 1
                    };
                return node2;
            }
            if (node2 == null)
            {
                if (node1 != null)
                    node1 = this.Add(node1, value, ref wasAdded, ref wasSuccessful);
                else
                    node1 = new AVLTree<T>.Node<T>()
                    {
                        Data = value,
                        Balance = 0,
                        Left = (AVLTree<T>.Node<T>)null,
                        Right = (AVLTree<T>.Node<T>)null,
                        Height = 1
                    };
                return node1;
            }
            int height1 = node1.Height;
            int height2 = node2.Height;
            if (height1 == height2)
            {
                AVLTree<T>.Node<T> node = new AVLTree<T>.Node<T>()
                {
                    Data = value,
                    Left = node1,
                    Right = node2,
                    Balance = 0,
                    Height = 1 + height1
                };
                node1.Parent = node;
                node2.Parent = node;
                return node;
            }
            return height1 > height2 ? this.ConcatImpl(node1, node2, value, ref wasAdded) : this.ConcatImpl(node2, node1, value, ref wasAdded);
        }

        private AVLTree<T>.Node<T> Split(
          AVLTree<T>.Node<T> elem,
          T data,
          ref AVLTree<T>.Node<T> splitLeftTree,
          ref AVLTree<T>.Node<T> splitRightTree,
          AVLTree<T>.SplitOperationMode mode,
          ref bool wasFound)
        {
            bool wasAdded = false;
            bool wasSuccessful = false;
            int num = this.comparer.Compare(data, elem.Data);
            if (num < 0)
            {
                this.Split(elem.Left, data, ref splitLeftTree, ref splitRightTree, mode, ref wasFound);
                if (wasFound)
                {
                    if (elem.Right != null)
                        elem.Right.Parent = (AVLTree<T>.Node<T>)null;
                    splitRightTree = this.ConcatAtJunctionPoint(splitRightTree, elem.Right, elem.Data);
                }
            }
            else if (num > 0)
            {
                this.Split(elem.Right, data, ref splitLeftTree, ref splitRightTree, mode, ref wasFound);
                if (wasFound)
                {
                    if (elem.Left != null)
                        elem.Left.Parent = (AVLTree<T>.Node<T>)null;
                    splitLeftTree = this.ConcatAtJunctionPoint(elem.Left, splitLeftTree, elem.Data);
                }
            }
            else
            {
                wasFound = true;
                splitLeftTree = elem.Left;
                splitRightTree = elem.Right;
                if (splitLeftTree != null)
                    splitLeftTree.Parent = (AVLTree<T>.Node<T>)null;
                if (splitRightTree != null)
                    splitRightTree.Parent = (AVLTree<T>.Node<T>)null;
                switch (mode)
                {
                    case AVLTree<T>.SplitOperationMode.IncludeSplitValueToLeftSubtree:
                        splitLeftTree = this.Add(splitLeftTree, elem.Data, ref wasAdded, ref wasSuccessful);
                        break;
                    case AVLTree<T>.SplitOperationMode.IncludeSplitValueToRightSubtree:
                        splitRightTree = this.Add(splitRightTree, elem.Data, ref wasAdded, ref wasSuccessful);
                        break;
                }
            }
            return elem;
        }

        public delegate void VisitNodeHandler<TNode>(TNode node, int level);

        public enum SplitOperationMode
        {
            IncludeSplitValueToLeftSubtree,
            IncludeSplitValueToRightSubtree,
            DoNotIncludeSplitValue,
        }

        public class Node<TElem>
        {
            public AVLTree<T>.Node<TElem> Left { get; set; }

            public AVLTree<T>.Node<TElem> Right { get; set; }

            public TElem Data { get; set; }

            public int Balance { get; set; }

            public int Height { get; set; }

            public AVLTree<T>.Node<TElem> Parent { get; set; }

            public void Visit(
              AVLTree<T>.VisitNodeHandler<AVLTree<T>.Node<TElem>> visitor,
              int level)
            {
                if (visitor == null)
                    return;
                if (this.Left != null)
                    this.Left.Visit(visitor, level + 1);
                visitor(this, level);
                if (this.Right == null)
                    return;
                this.Right.Visit(visitor, level + 1);
            }
        }
    }
}
