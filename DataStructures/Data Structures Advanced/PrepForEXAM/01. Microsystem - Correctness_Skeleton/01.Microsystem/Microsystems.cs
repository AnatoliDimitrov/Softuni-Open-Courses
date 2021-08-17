using System.Linq;
using Wintellect.PowerCollections;

namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;

    public class Microsystems : IMicrosystem
    {
        private Dictionary<int, Computer> numbers;
        private Dictionary<Brand, List<Computer>> brands;
        private Dictionary<string, List<Computer>> colors;
        private Dictionary<double, List<Computer>> screenSizes;
        private Dictionary<double, List<Computer>> price;
        private OrderedSet<double> prices;

        public Microsystems()
        {
            this.numbers = new Dictionary<int, Computer>();
            this.brands = new Dictionary<Brand, List<Computer>>();
            this.colors = new Dictionary<string, List<Computer>>();
            this.screenSizes = new Dictionary<double, List<Computer>>();
            this.price = new Dictionary<double, List<Computer>>();
            this.prices = new OrderedSet<double>();
        }

        public void CreateComputer(Computer computer)
        {
            if (this.numbers.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            this.numbers[computer.Number] = computer;

            if (!this.brands.ContainsKey(computer.Brand))
            {
                this.brands[computer.Brand] = new List<Computer>();
            }

            this.brands[computer.Brand].Add(computer);

            if (!this.colors.ContainsKey(computer.Color))
            {
                this.colors[computer.Color] = new List<Computer>();
            }

            this.colors[computer.Color].Add(computer);

            if (!this.screenSizes.ContainsKey(computer.ScreenSize))
            {
                this.screenSizes[computer.ScreenSize] = new List<Computer>();
            }

            this.screenSizes[computer.ScreenSize].Add(computer);

            if (!this.price.ContainsKey(computer.Price))
            {
                this.price[computer.Price] = new List<Computer>();
            }

            this.price[computer.Price].Add(computer);

            this.prices.Add(computer.Price);
        }

        public bool Contains(int number)
        {
            return this.numbers.ContainsKey(number);
        }

        public int Count()
        {
            return this.numbers.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.numbers.ContainsKey(number))
            {
               throw new ArgumentException();
            }

            return this.numbers[number];
        }

        public void Remove(int number)
        {
            if (!this.numbers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var comp = this.numbers[number];

            this.numbers.Remove(number);
            this.brands[comp.Brand].Remove(comp);
            this.colors[comp.Color].Remove(comp);
            this.screenSizes[comp.ScreenSize].Remove(comp);
            this.price[comp.Price].Remove(comp);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!this.brands.ContainsKey(brand) || this.brands[brand].Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var comp in this.brands[brand])
            {
                this.numbers.Remove(comp.Number);
                this.colors[comp.Color].Remove(comp);
                this.screenSizes[comp.ScreenSize].Remove(comp);
                this.price[comp.Price].Remove(comp);
            }

            this.brands.Remove(brand);
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!this.numbers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            if (this.numbers[number].RAM < ram)
            {
                this.numbers[number].RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!this.brands.ContainsKey(brand))
            {
                return new List<Computer>();
            }

            return this.brands[brand].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            if (!this.screenSizes.ContainsKey(screenSize))
            {
                return new List<Computer>();
            }

            return this.screenSizes[screenSize].OrderByDescending(c => c.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            if (!this.colors.ContainsKey(color))
            {
                return new List<Computer>();
            }

            return this.colors[color].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            var list = new List<Computer>();

            var range = this.prices.Range(minPrice, true, maxPrice, true);

            foreach (var comps in range)
            {
                if (this.price.ContainsKey(comps))
                {
                    foreach (var comp in this.price[comps])
                    {
                        list.Add(comp);
                    }
                }
            }

            return list.OrderByDescending(c => c.Price);
        }
    }
}
