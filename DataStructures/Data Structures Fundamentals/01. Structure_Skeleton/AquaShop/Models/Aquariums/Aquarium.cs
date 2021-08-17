using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private ICollection<IDecoration> decorations;
        private ICollection<IFish> fish;
        private string name;

        protected Aquarium(string name, int capacity)
        {
            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
            this.Name = name;
            this.Capacity = capacity;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }

                this.name = value;
            }
        }

        public int Capacity { get; }

        public int Comfort => CalculateComfort();

        public ICollection<IDecoration> Decorations => this.decorations;

        public ICollection<IFish> Fish => this.fish;

        public void AddFish(IFish fish)
        {
            if (this.fish.Count == this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void Feed()
        {
            foreach (var currentFish in this.fish)
            {
                currentFish.Eat();
            }
        }

        public string GetInfo()
        {

            StringBuilder result = new StringBuilder();

            result.AppendLine($"{this.Name} ({this.GetType().Name}):");
            if (!this.fish.Any())
            {
                result.AppendLine("Fish: none");
            }
            else
            {
                result.AppendLine($"Fish: {String.Join(", ", this.fish.Select(f => f.Name))}");
            }

            result.AppendLine($"Decorations: {this.decorations.Count}");
            result.Append($"Comfort: {this.Comfort}");

            return result.ToString();
        }

        private int CalculateComfort()
        {
            return this.Decorations
                .Sum(d => d.Comfort);
        }
    }
}
