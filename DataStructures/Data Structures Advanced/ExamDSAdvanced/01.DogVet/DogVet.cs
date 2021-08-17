using System.Linq;

namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;

    public class DogVet : IDogVet
    {
        Dictionary<string, Dog> dogs = new Dictionary<string, Dog>();
        Dictionary<string, Dog> dogNameOwnerId = new Dictionary<string, Dog>();

        public void AddDog(Dog dog, Owner owner)
        {
            if (dogs.ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }
            dogs.Add(dog.Id, dog);

            var nameId = dog.Name + "||" + owner.Id;

            if (dogNameOwnerId.ContainsKey(nameId))
            {
                throw new ArgumentException();
            }

            dogNameOwnerId.Add(nameId, dog);

            dog.Owner = owner;

        }

        public bool Contains(Dog dog)
        {
            return dogs.ContainsKey(dog.Id);
        }

        public int Size => dogs.Count;

        public Dog GetDog(string name, string ownerId)
        {
            var nameId = name + "||" + ownerId;

            if (!dogNameOwnerId.ContainsKey(nameId))
            {
                throw new ArgumentException();
            }

            return dogNameOwnerId[nameId];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            var nameId = name + "||" + ownerId;

            Dog dog = GetDog(name, ownerId);

            dogs.Remove(dog.Id);
            dogNameOwnerId.Remove(nameId);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            var list = dogs
                .Values
                .Where(d => d.Owner.Id == ownerId)
                .ToList();

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            var list = new List<Dog>();

            foreach (var dog in dogs)
            {
                if (dog.Value.Breed == breed)
                {
                    list.Add(dog.Value);
                }
            }

            if (list.Count == 0)
            {
                throw  new ArgumentException();

            }

            return list;
        }

        public void Vaccinate(string name, string ownerId)
        {
            Dog dog = GetDog(name, ownerId);
            dog.Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            var oldnameId = oldName + "||" + ownerId;

            Dog dog = GetDog(oldName, ownerId);

            dogNameOwnerId.Remove(oldnameId);

            var newNameId = newName + "||" + ownerId;

            dog.Name = newName;

            if (dogNameOwnerId.ContainsKey(newNameId))
            {
                throw new ArgumentException();
            }

            dogNameOwnerId.Add(newNameId, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            var list = dogs.Values
                .Where(d => d.Age == age).ToList();

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            return dogs.Values
                .Where(d => d.Age >= lo && d.Age <= hi).ToList();
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {

            return dogs.Values
                .OrderBy(d => d.Age)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.Owner.Name);
        }
    }
}