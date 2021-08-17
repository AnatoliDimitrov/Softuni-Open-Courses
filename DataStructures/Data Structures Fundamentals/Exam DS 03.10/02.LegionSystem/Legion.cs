using System.Linq;
using Wintellect.PowerCollections;

namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> enemies;

        public Legion()
        {
            this.enemies = new OrderedSet<IEnemy>();
        }

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            IEnemy result = null;

            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed == speed)
                {
                    result = enemy;
                    break;
                }
            }

            return result;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> result = new List<IEnemy>();

            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed > speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetFastest()
        {
            CheckForEmtyList(this.enemies);

            return this.enemies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies
                .OrderByDescending(e => e.Health)
                .ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> result = new List<IEnemy>();

            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed < speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetSlowest()
        {
            CheckForEmtyList(this.enemies);

            return this.enemies.GetLast();
        }

        public void ShootFastest()
        {
            CheckForEmtyList(this.enemies);

            this.enemies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            CheckForEmtyList(this.enemies);

            this.enemies.RemoveLast();
        }

        private void CheckForEmtyList(OrderedSet<IEnemy> orderedSet)
        {
            if (this.enemies.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
