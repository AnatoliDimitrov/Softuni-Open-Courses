namespace _02.FitGym
{
    public class Trainer
    {
        public Trainer(int id, string name, int popularity)
        {
            this.Id = id;
            this.Name = name;
            this.Popularity = popularity;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public override bool Equals(object obj)
        {
            Trainer other = (Trainer)obj;

            if (other == null || this.Id == null)
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