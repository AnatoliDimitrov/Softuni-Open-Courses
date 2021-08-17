namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> waepons;

        public Inventory()
        {
                this.waepons = new List<IWeapon>();
        }

        public int Capacity => this.waepons.Count;

        public void Add(IWeapon weapon)
        {
            this.waepons.Add(weapon);
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.waepons.Count; i++)
            {
                if (this.waepons[i].Id == id)
                {
                    return this.waepons[i];
                }
            }

            return null;
        }

        public bool Contains(IWeapon weapon)
        {
            return this.waepons.Contains(weapon);
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            int index = this.waepons.IndexOf(weapon);

            if (index >= 0)
            {
                var current = this.waepons[index];
                if (this.waepons[index].Ammunition + ammunition > current.MaxCapacity)
                {
                    this.waepons[index].Ammunition = current.MaxCapacity;
                    return this.waepons[index].Ammunition;
                }
                else
                {
                    this.waepons[index].Ammunition += ammunition;
                    return this.waepons[index].Ammunition;
                }
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            int index = this.waepons.IndexOf(weapon);

            if (index >= 0)
            {
                var current = this.waepons[index];
                if (this.waepons[index].Ammunition >= ammunition)
                {
                    this.waepons[index].Ammunition -= ammunition;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }

        public IWeapon RemoveById(int id)
        {
            int index = -1;

            for (int i = 0; i < this.waepons.Count; i++)
            {
                if (this.waepons[i].Id == id)
                {
                    index = i;
                    break; ;
                }
            }

            if (index >= 0)
            {
                var result = this.waepons[index];
                this.waepons.RemoveAt(index);
                return result;
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }

        public void Clear()
        {
            this.waepons.Clear();
        }
        
        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < waepons.Count; i++)
            {
                if (this.waepons[i].Category == category)
                {
                    this.waepons[i].Ammunition = 0;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.waepons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int RemoveHeavy()
        {
            int count = 0;

            for (int i = 0; i < this.waepons.Count; i++)
            {
                if ((int)this.waepons[i].Category == 2)
                {
                    count++;
                    this.waepons.RemoveAt(i);
                    i--;
                }
            }

            return count;
        }

        public List<IWeapon> RetrieveAll()
        {
            var list = new List<IWeapon>(this.Capacity);

            list.AddRange(this.waepons);

            return list;
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>();

            for (int i = 0; i < this.waepons.Count; i++)
            {
                if ((int)this.waepons[i].Category >= (int)lower && (int)this.waepons[i].Category <= (int)upper)
                {
                    result.Add(this.waepons[i]);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            var firstEntity = this.waepons.IndexOf(firstWeapon);
            if (firstEntity == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
            var secondEntity = this.waepons.IndexOf(secondWeapon);
            if (secondEntity == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (this.waepons[firstEntity].Category == this.waepons[secondEntity].Category)
            {
                var temp = this.waepons[firstEntity];
                this.waepons[firstEntity] = this.waepons[secondEntity];
                this.waepons[secondEntity] = temp;
            }

        }
    }
}
