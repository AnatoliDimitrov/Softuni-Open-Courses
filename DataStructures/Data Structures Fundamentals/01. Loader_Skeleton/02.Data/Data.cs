using System.Linq;
using _02.Data.Models;
using Wintellect.PowerCollections;

namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> entities;

        public Data()
        {
            this.entities = new OrderedBag<IEntity>();
        }

        public int Size => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);

            var parent = this.GetById((int)entity.ParentId);

            if (parent != null)
            {
                parent.AddChild(entity);
            }
        }

        public IRepository Copy()
        {
            return (IRepository)this.MemberwiseClone();
        }

        public IEntity DequeueMostRecent()
        {
            CheckForEmptyBag();

            return this.entities.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return this.entities.ToList();
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (type != nameof(Invoice) && type != nameof(User) && type != nameof(StoreClient))
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            List<IEntity> result = new List<IEntity>();

            foreach (var entity in this.entities)
            {
                var entityType = entity.GetType().Name;

                if (entityType == type)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEntity GetById(int id)
        {

            if (id < 0 || id >= this.Size)
            {
                return null;
            }

            return this.entities[this.Size - 1 - id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            //foreach (var parent in this.entities)
            //{
            //    if (parent.Id == parentId)
            //    {
            //        return parent.Children;
            //    }
            //}

            //return new List<IEntity>();

            return this.entities[this.Size - 1 - parentId].Children;
        }

        public IEntity PeekMostRecent()
        {
            CheckForEmptyBag();

            return this.entities.GetFirst();
        }

        private void CheckForEmptyBag()
        {
            if (this.entities.Count == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}
