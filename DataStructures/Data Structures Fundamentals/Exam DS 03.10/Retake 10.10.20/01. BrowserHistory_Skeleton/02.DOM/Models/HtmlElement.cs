namespace _02.DOM.Models
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;

    public class HtmlElement : IHtmlElement
    {
        public HtmlElement(ElementType type, params IHtmlElement[] children)
        {
            this.Children = new List<IHtmlElement>();
            this.Attributes = new Dictionary<string, string>();
            this.Parent = null;

            this.Type = type;

            for (int i = 0; i < children.Length; i++)
            {
                var child = children[i];
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public ElementType Type { get; set; }

        public IHtmlElement Parent { get; set; }

        public List<IHtmlElement> Children { get; private set; }

        public Dictionary<string, string> Attributes { get; private set; }

        public void RemoveChild(IHtmlElement child)
        {
            this.Children.Remove(child);
        }

        public bool AddAttr(string key, string value)
        {
            if (this.Attributes.ContainsKey(key))
            {
                return false;
            }
            this.Attributes.Add(key, value);
            return true;
        }

        public bool RemoveAttr(string key)
        {
            if (this.Attributes.ContainsKey(key))
            {
                this.Attributes.Remove(key);
                return true;
            }
            return false;
        }
    }
}
