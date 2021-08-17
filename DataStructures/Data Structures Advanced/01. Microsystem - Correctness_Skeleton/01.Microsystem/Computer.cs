using System;

namespace _01.Microsystem
{
    public class Computer //: IComparable<Computer>
    {
        public Computer(int number, Brand brand, double price, double screenSize, string color)
        {
            this.Number = number;
            this.RAM = 8;
            this.Brand = brand;
            this.Price = price;
            this.ScreenSize = screenSize;
            this.Color = color;
        }
        public int Number { get; set; }

        public int RAM { get; set; }

        public Brand Brand { get; set; }

        public double Price { get; set; }

        public double ScreenSize { get; set; }

        public string Color { get; set; }

        //public int CompareTo(object other)
        //{
        //    Computer comp = (Computer)other;

        //    return comp.Price.CompareTo(this.Price);
        //}


        //public int CompareTo(Computer other)
        //{
        //    return other.Price.CompareTo(this.Price);
        //}

        public override bool Equals(object obj)
        {
            Computer other = (Computer)obj;

            return other.Brand == this.Brand &&
                   other.Price == this.Price &&
                   other.RAM == this.RAM &&
                   other.ScreenSize == this.ScreenSize &&
                   other.Number == this.Number &&
                   other.Color == this.Color;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
