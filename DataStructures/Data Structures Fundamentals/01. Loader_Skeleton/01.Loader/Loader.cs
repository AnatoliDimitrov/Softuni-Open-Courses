namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;

        public Loader()
        {
            this.entities = new List<IEntity>();
        }

        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (entity.Id == this.entities[i].Id)
                {
                    return true;
                }
            }

            return false;
        }

        public IEntity Extract(int id)
        {
            IEntity result = null;

            for (int i = 0; i < this.EntitiesCount; i++)
            {

                if (this.entities[i].Id == id)
                {
                    result = this.entities[i];
                    this.entities.RemoveAt(i);
                    break;
                }
            }

            return result;
        }

        public IEntity Find(IEntity entity)
        {
            IEntity result = null;
            int id = entity.Id;

            for (int i = 0; i < this.EntitiesCount; i++)
            {

                if (this.entities[i].Id == id)
                {
                    result = this.entities[i];
                    break;
                }
            }

            return result;
        }

        public List<IEntity> GetAll()
        {
            return this.entities;
        }

        public void RemoveSold()
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var entity = this.entities[i];

                if (entity.Status == BaseEntityStatus.Sold)
                {
                    this.entities.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int id = oldEntity.Id;

            for (int i = 0; i < this.EntitiesCount; i++)
            {

                if (this.entities[i].Id == id)
                {
                    this.entities[i] = newEntity;
                    return;
                }
            }

            throw new InvalidOperationException("Entity not found");
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            List<IEntity> result = new List<IEntity>();

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var status = this.entities[i].Status;

                if (status >= lowerBound && status <= upperBound)
                {
                    result.Add(this.entities[i]);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            var firstEntity = this.entities.IndexOf(first);
            if (firstEntity == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
            var secondEntity = this.entities.IndexOf(second);
            if (secondEntity == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            var temp = this.entities[firstEntity];
            this.entities[firstEntity] = this.entities[secondEntity];
            this.entities[secondEntity] = temp;
        }

        public IEntity[] ToArray()
        {
            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var entity = this.entities[i];

                if (entity.Status == oldStatus)
                {
                    entity.Status = newStatus;
                }
            }
        }



        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
