namespace _01._BrowserHistory
{
    using _01._BrowserHistory.Interfaces;

    public class Link : ILink
    {
        public Link(string url, int loadingTime)
        {
            this.Url = url;
            this.LoadingTime = loadingTime;
        }

        public string Url { get; set; }
        public int LoadingTime { get; set; }

        public override string ToString()
        {
            return $"-- {this.Url} {this.LoadingTime}s";
        }

        public override bool Equals(object obj)
        {
            ILink link = (ILink) obj;

            return link.Url.Equals(this.Url);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo()
        {
            return 0;
        }
    }
}
