using _02.DOM.Interfaces;
using _02.DOM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _02.DOM
{
    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            this.Root = new HtmlElement(
                ElementType.Document,
                    new HtmlElement(ElementType.Html,
                        new HtmlElement(ElementType.Head),
                        new HtmlElement(ElementType.Body)
                    )
            );
        }

        public IHtmlElement Root { get; private set; }

        public bool Contains(IHtmlElement htmlElement)
        {
            return this.FindHtmlElement(htmlElement) != null;
        }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl.Type == type)
                {
                    return currentEl;
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var result = new List<IHtmlElement>();

            this.FindElementsByTypeDfs(this.Root, type, result);

            return result;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            this.CheckIfHtmlElementExists(parent);

            parent.Children.Insert(0, child);
            child.Parent = parent;
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            this.CheckIfHtmlElementExists(parent);

            parent.Children.Add(child);
            child.Parent = parent;
        }

        public void Remove(IHtmlElement htmlElement)
        {
            this.CheckIfHtmlElementExists(htmlElement);
            this.RemoveReferences(htmlElement, htmlElement.Parent);
        }


        public void RemoveAll(ElementType elementType)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl.Type == elementType)
                {
                    var parent = currentEl.Parent;

                    this.RemoveReferences(currentEl, parent);
                }
                else
                {
                    foreach (var child in currentEl.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            this.CheckIfHtmlElementExists(htmlElement);

            if (!htmlElement.Attributes.ContainsKey(attrKey))
            {
                htmlElement.Attributes.Add(attrKey, attrValue);
                return true;
            }

            return false;
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            this.CheckIfHtmlElementExists(htmlElement);

            if (htmlElement.Attributes.ContainsKey(attrKey))
            {
                htmlElement.Attributes.Remove(attrKey);
                return true;
            }

            return false;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();
                var attributes = currentEl.Attributes;

                if (attributes.ContainsKey("id"))
                {
                    if (attributes["id"] == idValue)
                    {
                        return currentEl;
                    }
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            this.CreateTextDfs(this.Root, result, 0);

            return result.ToString();
        }

        private void CreateTextDfs(IHtmlElement current, StringBuilder result, int depth)
        {
            result
                .AppendLine($"{new string(' ', depth)}{current.Type.ToString()}");

            foreach (var child in current.Children)
            {
                this.CreateTextDfs(child, result, depth + 2);
            }
        }

        private void FindElementsByTypeDfs(IHtmlElement current,
            ElementType type,
            List<IHtmlElement> result)
        {
            foreach (var child in current.Children)
            {
                this.FindElementsByTypeDfs(child, type, result);
            }

            if (current.Type == type)
            {
                result.Add(current);
            }
        }

        private void CheckIfHtmlElementExists(IHtmlElement element)
        {
            var found = this.FindHtmlElement(element);

            if (found == null)
            {
                throw new InvalidOperationException("Html element not found in DOM tree!");
            }
        }

        private IHtmlElement FindHtmlElement(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();

            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currentEl = queue.Dequeue();

                if (currentEl == htmlElement)
                {
                    return currentEl;
                }

                foreach (var child in currentEl.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void RemoveReferences(IHtmlElement currentEl, IHtmlElement parent)
        {
            parent.Children.Remove(currentEl);
            currentEl.Parent = null;
            currentEl.Children.Clear();
        }
    }
}
