using System.Linq;
using Wintellect.PowerCollections;

namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;

    public class Microsystems : IMicrosystem
    {
        private Dictionary<int, Computer> ByNumber;
        private Dictionary<Brand, OrderedBag<Computer>> ByBrand;
        private Dictionary<double, OrderedBag<Computer>> ByScreenSize;
        private Dictionary<string, OrderedBag<Computer>> ByColor;
        //private OrderedDictionary<double, OrderedBag<Computer>> ByPrice;

        public Microsystems()
        {
            this.ByNumber = new Dictionary<int, Computer>();
            this.ByBrand = new Dictionary<Brand, OrderedBag<Computer>>();
            this.ByScreenSize = new Dictionary<double, OrderedBag<Computer>>();
            this.ByColor = new Dictionary<string, OrderedBag<Computer>>();
            //this.ByPrice = new OrderedDictionary<double, OrderedBag<Computer>>();
        }

        public void CreateComputer(Computer computer)
        {
            if (ByNumber.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            ByNumber.Add(computer.Number, computer);
            if (!ByBrand.ContainsKey(computer.Brand))
            {
                ByBrand[computer.Brand] = new OrderedBag<Computer>(CompareByPriceDescending);
            }

            ByBrand[computer.Brand].Add(computer);

            if (!ByScreenSize.ContainsKey(computer.ScreenSize))
            {
                ByScreenSize[computer.ScreenSize] = new OrderedBag<Computer>(CompareByNumberDescending);
            }

            ByScreenSize[computer.ScreenSize].Add(computer);

            if (!ByColor.ContainsKey(computer.Color))
            {
                ByColor[computer.Color] = new OrderedBag<Computer>(CompareByPriceDescending);
            }

            ByColor[computer.Color].Add(computer);

            //if (!ByPrice.ContainsKey(computer.Price))
            //{
            //    this.ByPrice[computer.Price] = new OrderedBag<Computer>(CompareByPriceDescending);
            //}

            //ByPrice[computer.Price].Add(computer);

        }

        public bool Contains(int number)
        {
            return this.ByNumber.ContainsKey(number);
        }

        public int Count()
        {
            return this.ByNumber.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.ByNumber.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            return this.ByNumber[number];
        }

        public void Remove(int number)
        {
            if (!this.ByNumber.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var comp = this.ByNumber[number];
            this.ByNumber.Remove(number);
            this.ByBrand[comp.Brand].Remove(comp);
            this.ByColor[comp.Color].Remove(comp);
            this.ByScreenSize[comp.ScreenSize].Remove(comp);
            //this.ByPrice[comp.Price].Remove(comp);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!this.ByBrand.ContainsKey(brand))
            {
                throw new ArgumentException();
            }

            if (this.ByBrand[brand].Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var computer in ByBrand[brand])
            {
                this.ByNumber.Remove(computer.Number);
                this.ByScreenSize[computer.ScreenSize].Remove(computer);
                this.ByColor[computer.Color].Remove(computer);
                //this.ByPrice[computer.Price].Remove(computer);
            }

            this.ByBrand.Remove(brand);
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!this.ByNumber.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var comp = ByNumber[number];

            if (ram > comp.RAM)
            {
                comp.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (this.ByBrand.ContainsKey(brand))
            {
                return this.ByBrand[brand];
            }

            return new List<Computer>();
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            if (this.ByScreenSize.ContainsKey(screenSize))
            {
                return this.ByScreenSize[screenSize];
            }

            return new List<Computer>();
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            if (this.ByColor.ContainsKey(color))
            {
                return this.ByColor[color];
            }

            return new List<Computer>();
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            //OrderedBag<Computer> result = new OrderedBag<Computer>(CompareByPriceDescending);

            //foreach (var comp in this.ByPrice.Range(minPrice, true, maxPrice, true))
            //{
            //    foreach (var value in comp.Value)
            //    {
            //        result.Add(value);
            //    }
            //}

            //var sorted = ByNumber.OrderByDescending(c => c.Value.Price);

            var list = new List<Computer>();

            foreach (var comp in ByNumber)
            {
                if (comp.Value.Price >= minPrice && comp.Value.Price <= maxPrice)
                {
                    list.Add(comp.Value);
                }
            }

            return list.OrderByDescending(c => c.Price);
        }

        private static readonly Comparison<Computer> CompareByPriceDescending = new Comparison<Computer>(
            (Computer x, Computer y) =>
            {
                int cmp = y.Price.CompareTo(x.Price);
                return cmp;
            });

        private static readonly Comparison<Computer> CompareByNumberDescending = new Comparison<Computer>(
            (Computer x, Computer y) =>
            {
                int cmp = y.Number.CompareTo(x.Number);
                return cmp;
            });

        private static readonly Comparison<double> CompareByPriceDescendingDouble = new Comparison<double>(
            (double x, double y) =>
            {
                int cmp = y.CompareTo(x);
                return cmp;
            });
    }
}
