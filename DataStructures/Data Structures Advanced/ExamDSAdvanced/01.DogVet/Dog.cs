namespace _01.DogVet
{
    using System;

    public class Dog
    {
        public Dog(string id, string name, Breed breed, int age, int vaccines)
        {
            this.Id = id;
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.Vaccines = vaccines;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public Breed Breed { get; set; }

        public int Age { get; set; }

        public int Vaccines { get; set; }

        public Owner Owner { get; set; }

        public override bool Equals(object obj)
        {
            Dog other = (Dog) obj;
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