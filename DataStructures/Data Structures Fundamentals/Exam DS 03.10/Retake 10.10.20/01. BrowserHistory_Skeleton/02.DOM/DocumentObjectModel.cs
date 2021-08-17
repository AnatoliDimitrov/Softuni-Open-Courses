using System.Collections;
using System.Text;

namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.Root = new HtmlElement(ElementType.Document, new HtmlElement(ElementType.Html, new HtmlElement(ElementType.Head), new HtmlElement(ElementType.Body)));
        }

        public IHtmlElement Root { get; private set; }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree.Type == type)
                {
                    return tree;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            List<IHtmlElement> result = new List<IHtmlElement>();

            DfsElementsByType(result, this.Root, type);

            return result;
        }

        private void DfsElementsByType(List<IHtmlElement> result, IHtmlElement tree, ElementType type)
        {
            foreach (var child in tree.Children)
            {
                DfsElementsByType(result, child, type);
            }

            if (tree.Type == type)
            {
                result.Add(tree);
            }
        }

        public bool Contains(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == htmlElement)
                {
                    return true;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            return false;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == parent)
                {
                    exist = true;
                    break;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            if (exist)
            {
                child.Parent = parent;
                parent.Children.Insert(0, child);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == parent)
                {
                    exist = true;
                    break;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            if (exist)
            {
                child.Parent = parent;
                parent.Children.Add(child);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Remove(IHtmlElement htmlElement)
        {
            if (this.Root == htmlElement)
            {
                this.Root = null;
            }

            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == htmlElement)
                {
                    exist = true;
                    break;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }
            
            if (exist)
            {
                var parent = htmlElement.Parent;
                htmlElement.Parent = null;
                parent.Children.Remove(htmlElement);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void RemoveAll(ElementType elementType)
        {
            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree.Type == elementType)
                {
                    var parent = tree.Parent;
                    tree.Parent = null;
                    parent.Children.Remove(tree);
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == htmlElement)
                {
                    exist = true;
                    break;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            if (exist)
            {
                HtmlElement element = (HtmlElement) htmlElement;
                return element.AddAttr(attrKey, attrValue);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {

            bool exist = false;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree == htmlElement)
                {
                    exist = true;
                    break;
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            if (exist)
            {
                HtmlElement element = (HtmlElement)htmlElement;
                return element.RemoveAttr(attrKey);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var tree = queue.Dequeue();
                if (tree.Attributes.ContainsKey("id"))
                {
                    if (tree.Attributes["id"] == idValue)
                    {
                        return tree;
                    }
                }

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    queue.Enqueue(tree.Children[i]);
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            int indenter = 0;

            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {

                var tree = queue.Dequeue();

                result.AppendLine(new string(' ', indenter) + tree.Type);

                for (int i = 0; i < tree.Children.Count; i++)
                {
                    if (i == 0)
                    {
                        indenter += 2;
                    }

                    queue.Enqueue(tree.Children[i]);
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
