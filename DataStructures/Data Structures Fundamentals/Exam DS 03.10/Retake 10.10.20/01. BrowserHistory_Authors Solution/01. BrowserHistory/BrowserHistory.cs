using _01._BrowserHistory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _01._BrowserHistory
{
    public class BrowserHistory : IHistory
    {
        private Node<ILink> _head;
        private Node<ILink> _tail;

        public int Size { get; private set; }

        public void Open(ILink link)
        {
            var toInsert = new Node<ILink>(link);

            if (this.Size == 0)
            {
                this._head = this._tail = toInsert;
            }
            else
            {
                this._head.Previous = toInsert;
                toInsert.Next = this._head;
                this._head = toInsert;
            }

            this.Size++;
        }

        public ILink GetByUrl(string url)
        {
            var current = this._head;

            while (current != null)
            {
                if (current.Value.Url == url)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        public bool Contains(ILink link)
        {
            return this.GetByUrl(link.Url) != null;
        }

        public ILink LastVisited()
        {
            this.EnsureNotEmpty();

            return this._head.Value;
        }

        public ILink DeleteLast()
        {
            this.EnsureNotEmpty();

            var last = this._head;

            if (this.Size == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._head = this._head.Next;
                this._head.Previous = null;
            }

            this.Size--;
            return last.Value;
          
        }

        public ILink DeleteFirst()
        {
            this.EnsureNotEmpty();

            var first = this._tail;

            if (this.Size == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._tail = this._tail.Previous;
                this._tail.Next = null;
            }

            this.Size--;
            return first.Value;
        }

        public void Clear()
        {
            this._head = this._tail = null;
            this.Size = 0;
        }

        public int RemoveLinks(string url)
        {
            var current = this._head;
            int count = 0;

            while (current != null)
            {
                if (current.Value.Url.Contains(url))
                {
                    var previous = current.Previous;
                    var next = current.Next;

                    if (previous != null)
                    {
                        previous.Next = next;
                    }
                    else
                    {
                        this._head = this._head.Next;
                    }

                    if (next != null)
                    {
                        next.Previous = previous;
                    }
                    else
                    {
                        this._tail = this._tail.Previous;
                    }

                    count++;
                    this.Size--;
                }


                current = current.Next;
            }

            if (count == 0)
            {
                throw new InvalidOperationException("No links with the given url!");
            }

            return count;
        }

        public ILink[] ToArray()
        {
            ILink[] result = new ILink[this.Size];
            int index = 0;
            var current = this._head;

            while (current != null)
            {
                result[index++] = current.Value;

                current = current.Next;
            }

            return result;
        }

        public List<ILink> ToList()
        {
            List<ILink> result = new List<ILink>(this.Size);
            var current = this._head;

            while (current != null)
            {
                result.Add(current.Value);

                current = current.Next;
            }

            return result;
        }

        public string ViewHistory()
        {
            if (this.Size == 0)
            {
                return "Browser history is empty!";
            }

            StringBuilder output = new StringBuilder();
            var current = this._head;

            while (current != null)
            {
                output.AppendLine(current.Value.ToString());

                current = current.Next;
            }


            return output.ToString();
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Browser history is empty!");
            }
        }
    }
}
