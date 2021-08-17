using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class RoyaleArena : IArena
{
    private Dictionary<int, Battlecard> ids;
    //private Dictionary<CardType, LinkedList<Battlecard>> types;
    private Dictionary<string, HashSet<Battlecard>> names;
    //private Dictionary<double, HashSet<Battlecard>> swag;

    public RoyaleArena()
    {
        this.ids = new Dictionary<int, Battlecard>();
        //this.types = new Dictionary<CardType, LinkedList<Battlecard>>();
        this.names = new Dictionary<string, HashSet<Battlecard>>();
        //this.swag = new Dictionary<double, HashSet<Battlecard>>();
    }

    public void Add(Battlecard card)
    {
        this.ids[card.Id] = card;

        //if (!this.types.ContainsKey(card.Type))
        //{
        //    this.types[card.Type] = new LinkedList<Battlecard>();
        //}

        //this.types[card.Type].AddLast(card);

        if (!this.names.ContainsKey(card.Name))
        {
            this.names[card.Name] = new HashSet<Battlecard>();
        }

        this.names[card.Name].Add(card);

        //if (!this.swag.ContainsKey(card.Swag))
        //{
        //    this.swag[card.Swag] = new HashSet<Battlecard>();
        //}

        //this.swag[card.Swag].Add(card);
    }

    public bool Contains(Battlecard card)
    {
        return this.ids.ContainsKey(card.Id);
    }

    public int Count => this.ids.Count;

    public void ChangeCardType(int id, CardType type)
    {
        if (!this.ids.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.ids[id].Type = type;

        //var card = this.ids[id];

        //types[card.Type].Remove(card);

        //card.Type = type;

        //if (!this.types.ContainsKey(type))
        //{
        //    this.types[type] = new LinkedList<Battlecard>();
        //}

        //this.types[type].AddLast(card);
    }

    public Battlecard GetById(int id)
    {
        if (!this.ids.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.ids[id];
    }

    public void RemoveById(int id)
    {
        if (!this.ids.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        var card = this.ids[id];

        this.ids.Remove(id);

        //this.ids.Remove(id);

        //this.types[card.Type].Remove(card);

        this.names[card.Name].Remove(card);
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {
        //if (!this.types.ContainsKey(type) || this.types[type].Count == 0)
        //{
        //    throw new InvalidOperationException();
        //}

        var list = new List<Battlecard>();

        foreach (var kvp in ids)
        {
            var battlecard = kvp.Value; 

            if (battlecard.Type == type)
            {
                list.Add(battlecard);
            }
        }

        if (list.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return list
            .OrderByDescending(c => c.Damage)
            .ThenBy(c => c.Id);
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        //if (!this.types.ContainsKey(type) || this.types[type].Count == 0)
        //{
        //    throw new InvalidOperationException();
        //}

        var list = new List<Battlecard>();

        foreach (var kvp in ids)
        {
            var battlecard = kvp.Value;

            if (battlecard.Type == type && battlecard.Damage > lo && battlecard.Damage < hi)
            {
                list.Add(battlecard);
            }
        }

        if (list.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return list
            //.Where(k => k.Damage > lo && k.Damage <= hi)
            .OrderByDescending(c => c.Damage)
            .ThenBy(c => c.Id);
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        //if (!this.types.ContainsKey(type) || this.types[type].Count == 0)
        //{
        //    throw new InvalidOperationException();
        //}

        var list = new List<Battlecard>();

        foreach (var kvp in ids)
        {
            var battlecard = kvp.Value;

            if (battlecard.Type == type && battlecard.Damage <= damage)
            {
                list.Add(battlecard);
            }
        }

        if (list.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return list
            .Where(k => k.Damage <= damage)
            .OrderByDescending(c => c.Damage)
            .ThenBy(c => c.Id);
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        //if (!this.names.ContainsKey(name))
        //{
        //    throw new InvalidOperationException();
        //}

        var list = new List<Battlecard>();

        foreach (var kvp in ids)
        {
            var battlecard = kvp.Value;

            if (battlecard.Name == name)
            {
                list.Add(battlecard);
            }
        }

        if (list.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return list
            .OrderByDescending(c => c.Swag)
            .ThenBy(c => c.Id);
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        var list = new List<Battlecard>();

        foreach (var kvp in ids)
        {
            var battlecard = kvp.Value;

            if (battlecard.Name == name && battlecard.Swag >= lo && battlecard.Swag < hi)
            {
                list.Add(battlecard);
            }
        }

        if (list.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return list
            //.Where(c => c.Swag >= lo && c.Swag < hi)
            .OrderByDescending(c => c.Swag)
            .ThenBy(c => c.Id);
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        var list = new List<Battlecard>();

        foreach (var kvp in this.names)
        {
            list.Add(this.names[kvp.Key].OrderByDescending(c => c.Swag).First());
        }

        if (list.Count == 0)
        {
            return new List<Battlecard>();
            //throw  new InvalidOperationException();
        }

        return list;
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        if (n > this.ids.Count)
        {
            throw new InvalidOperationException();
        }

        return this.ids
            .Select(k => k.Value)
            .OrderBy(c => c.Swag)
            .ThenBy(c => c.Id)
            .Take(n);
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        return this.ids
            .Select(k => k.Value)
            .Where(c => c.Swag >= lo && c.Swag <= hi)
            .OrderBy(c => c.Swag);
    }

    public IEnumerator<Battlecard> GetEnumerator()
    {
        foreach (var kvp in this.ids)
        {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
