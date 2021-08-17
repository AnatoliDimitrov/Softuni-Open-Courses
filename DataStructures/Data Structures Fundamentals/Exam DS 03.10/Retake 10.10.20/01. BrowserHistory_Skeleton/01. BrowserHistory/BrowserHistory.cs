using System.Text;

namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private List<ILink> history;

        public BrowserHistory()
        {
            this.history = new List<ILink>();
        }

        public int Size => this.history.Count;

        public void Clear()
        {
            this.history = new List<ILink>();
        }

        public bool Contains(ILink link)
        {
            return this.history.Contains(link);
        }

        public ILink DeleteFirst()
        {
            EnsureNotEmpty();
            ILink link = this.history[0];
            this.history.RemoveAt(0);
            return link;
        }

        public ILink DeleteLast()
        {
            EnsureNotEmpty();
            ILink link = this.history[this.Size - 1];
            this.history.RemoveAt(this.Size - 1);
            return link;
        }

        public ILink GetByUrl(string url)
        {
            ILink result = null;

            for (int i = this.Size - 1; i >= 0; i--)
            {
                if (url == this.history[i].Url)
                {
                    result = this.history[i];
                    break;
                }
            }

            return result;
        }

        public ILink LastVisited()
        {
            EnsureNotEmpty();
            return this.history[this.Size - 1];
        }

        public void Open(ILink link)
        {
            this.history.Add(link);
        }

        public int RemoveLinks(string url)
        {
            int counter = 0;

            for (int i = 0; i < this.Size; i++)
            {
                if (this.history[i].Url.Contains(url))
                {
                    this.history.RemoveAt(i);
                    i--;
                    counter++;
                }
            }

            if (counter == 0)
            {
                throw new InvalidOperationException();
            }

            return counter;
        }

        public ILink[] ToArray()
        {
            ILink[] arr = new ILink[this.history.Count];

            for (int i = 0; i < this.Size; i++)
            {
                arr[i] = this.history[this.Size - 1 - i];
            }

            return arr;
        }

        public List<ILink> ToList()
        {
            List<ILink> arr = new List<ILink>();

            for (int i = 0; i < this.Size; i++)
            {
                arr.Add(this.history[this.Size - 1 - i]);
            }
            return arr;
        }

        public string ViewHistory()
        {
            StringBuilder result = new StringBuilder();

            for (int i = this.Size - 1; i >= 0; i--)
            {
                result.AppendLine(this.history[i].ToString());
            }

            if (result.Length == 0)
            {
                return "Browser history is empty!";
            }

            return result.ToString();
        }

        private void EnsureNotEmpty()
        {
            if (this.history.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
