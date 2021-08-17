namespace _01.DogVet
{
    public class Owner
    {
        public Owner(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Owner other = (Owner) obj;

            if (other == null)
            {
                return false;
            }
            return other.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}