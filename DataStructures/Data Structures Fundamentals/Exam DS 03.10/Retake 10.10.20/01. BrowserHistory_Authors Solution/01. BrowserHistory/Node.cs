namespace _01._BrowserHistory
{
    public class Node<ILink>
    {
        public Node(ILink value, Node<ILink> next = null, Node<ILink> prev = null)
        {
            this.Value = value;
            this.Next = next;
            this.Previous = prev;
        }

        public ILink Value { get; set; }

        public Node<ILink> Next { get; set; }

        public Node<ILink> Previous { get; set; }
    }
}
