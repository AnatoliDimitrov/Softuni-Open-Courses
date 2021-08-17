using System.Runtime.CompilerServices;
using Wintellect.PowerCollections;

namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RoyaleArena : IArena
    {
        Dictionary<int, BattleCard> deck = new Dictionary<int, BattleCard>();
        OrderedSet<BattleCard> byTypeAndDamage = new OrderedSet<BattleCard>(CompareByTypeAndDamageThenID);
        OrderedSet<BattleCard> byNameAndSwag = new OrderedSet<BattleCard>(CompareByNameAndSwagThenID);
        OrderedSet<BattleCard> bySwagDescending = new OrderedSet<BattleCard>(CompareSwagThenIDDescending);

        public void Add(BattleCard card)
        {

            if (deck.ContainsKey(card.Id))
            {
                return;
            }

            deck.Add(card.Id, card);
            byTypeAndDamage.Add(card);
            byNameAndSwag.Add(card);
            bySwagDescending.Add(card);
        }

        public bool Contains(BattleCard card)
        {
            if (!deck.ContainsKey(card.Id))
            {
                return false;
            }

            return deck[card.Id].Equals(card);
        }

        public int Count => deck.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            deck[id].Type = type;
        }

        public BattleCard GetById(int id)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            return deck[id];
        }

        public void RemoveById(int id)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            var card = deck[id];

            deck.Remove(card.Id);
            byTypeAndDamage.Remove(card);
            byNameAndSwag.Remove(card);
            bySwagDescending.Remove(card);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byTypeAndDamage)
            {
                if (card.Type == type)
                {
                    result.Add(card);
                }
            }

            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byTypeAndDamage)
            {
                if (card.Type == type && card.Damage > lo && card.Damage < hi)
                {
                    result.Add(card);
                }
            }
            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byTypeAndDamage)
            {
                if (card.Type == type && card.Damage <= damage)
                {
                    result.Add(card);
                }
            }
            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byNameAndSwag)
            {
                if (card.Name == name)
                {
                    result.Add(card);
                }
            }
            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byNameAndSwag)
            {
                if (card.Name == name && card.Swag > lo && card.Swag < hi)
                {
                    result.Add(card);
                }
            }
            if (result.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (bySwagDescending.Count < n)
            {
                throw new InvalidOperationException();
            }
            if (bySwagDescending.Count == n)
            {
                return bySwagDescending;
            }

            List<BattleCard> result = new List<BattleCard>(deck.Count);

            int counter = 0;

            foreach (var card in bySwagDescending)
            {
                if (counter == n)
                {
                    break;
                }

                result.Add(card);

                counter++;

            }

            return result;
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            List<BattleCard> result = new List<BattleCard>(deck.Count);

            foreach (var card in byNameAndSwag)
            {
                if (card.Swag >= lo && card.Swag <= hi)
                {
                    result.Add(card);
                }
            }

            result.Reverse();

            return result;
        }

        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var card in deck)
            {
                yield return card.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static readonly Comparison<BattleCard> CompareByTypeAndDamageThenID = new Comparison<BattleCard>(
            (BattleCard x, BattleCard y) =>
            {
                int cmp = x.Type.CompareTo(y.Type);

                if (cmp == 0)
                {
                    cmp = y.Damage.CompareTo(x.Damage);
                    if (cmp == 0)
                    {
                        cmp = x.Id.CompareTo(y.Id);
                    }
                }

                return cmp;
            });

        private static readonly Comparison<BattleCard> CompareByNameAndSwagThenID = new Comparison<BattleCard>(
            (BattleCard x, BattleCard y) =>
            {
                int cmp = x.Name.CompareTo(y.Name);

                if (cmp == 0)
                {
                    cmp = y.Swag.CompareTo(x.Swag);
                    if (cmp == 0)
                    {
                        cmp = x.Id.CompareTo(y.Id);
                    }
                }

                return cmp;
            });

        private static readonly Comparison<BattleCard> CompareSwagThenIDDescending = new Comparison<BattleCard>(
            (BattleCard x, BattleCard y) =>
            {
                int cmp = x.Swag.CompareTo(y.Swag);

                if (cmp == 0)
                {
                    cmp = x.Id.CompareTo(y.Id);
                }

                return cmp;
            });
    }
}