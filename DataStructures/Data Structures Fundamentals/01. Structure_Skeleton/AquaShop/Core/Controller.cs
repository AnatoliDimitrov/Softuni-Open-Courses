using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using AquaShop.Repositories;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Core.Contracts;
using AquaShop.Utilities.Messages;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorations;
        private ICollection<IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;

            if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == nameof(SaltwaterAquarium))
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            aquariums.Add(aquarium);

            return String.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;

            if (decorationType == nameof(Plant))
            {
                decoration = new Plant();
            }
            else if (decorationType == nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            this.decorations.Add(decoration);

            return String.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = aquariums
                     .FirstOrDefault(a => a.Name == aquariumName);

            IDecoration decoration = decorations.FindByType(decorationType);

            if (decoration == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }
            else
            {
                aquarium.AddDecoration(decoration);
                this.decorations.Remove(decoration);
                return String.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
            }
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fish;

            if (fishType == nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IAquarium aquarium = aquariums
                .FirstOrDefault(a => a.Name == aquariumName);

            if (aquarium.GetType().Name.StartsWith("S") && fish.GetType().Name.StartsWith("S"))
            {
                aquarium.AddFish(fish);
                return String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else if (aquarium.GetType().Name.StartsWith("F") && fish.GetType().Name.StartsWith("F"))
            {
                aquarium.AddFish(fish);
                return String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else
            {
                return OutputMessages.UnsuitableWater;
            }
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums
                .FirstOrDefault(a => a.Name == aquariumName);

            aquarium.Feed();

            return String.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums
                .FirstOrDefault(a => a.Name == aquariumName);

            decimal total = aquarium
                .Decorations.Sum(d => d.Price);

            total += aquarium
                .Fish.Sum(f => f.Price);

            return String.Format(OutputMessages.AquariumValue, aquariumName, total);
                
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                result.AppendLine(aquarium.GetInfo());
            }

            return result.ToString().TrimEnd();
        }
    }
}
