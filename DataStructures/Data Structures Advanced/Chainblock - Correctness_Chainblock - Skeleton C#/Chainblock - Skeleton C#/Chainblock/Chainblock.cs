using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Chainblock : IChainblock
{
    private Dictionary<int, Transaction> ids = new Dictionary<int, Transaction>();
    public int Count => ids.Count;

    public void Add(Transaction tx)
    {
        if (ids.ContainsKey(tx.Id))
        {
            return;
        }

        ids[tx.Id] = tx;
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!ids.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        ids[id].Status = newStatus;
    }

    public bool Contains(Transaction tx)
    {
        return ids.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return ids.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {

        var list = new List<Transaction>();

        foreach (var amountlist in ids)
        {
            if (amountlist.Value.Amount >= lo && amountlist.Value.Amount <= hi)
            {
                list.Add(amountlist.Value);
            }
        }

        return list;
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        var list = new List<Transaction>();

        foreach (var kvp in ids)
        {
            list.Add(kvp.Value);
        }

        return list.OrderByDescending(c => c.Amount).ThenBy(c => c.Id);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        var sorted = ids.Select(t => t.Value)
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.Amount)
            .Select(t => t.From)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {

        var sorted = ids.Select(t => t.Value)
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.Amount)
            .Select(t => t.To)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public Transaction GetById(int id)
    {
        if (!ids.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return ids[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {

        var sorted = ids.Select(t => t.Value)
            .Where(t => t.To == receiver && t.Amount >= lo && t.Amount < hi)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var sorted = ids.Select(t => t.Value)
            .Where(t => t.To == receiver)
            .OrderByDescending(t => t.Amount)
            .ThenBy(t => t.Id)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;

    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var sorted = ids.Select(t => t.Value)
            .Where(t => t.From == sender && t.Amount > amount)
            .OrderByDescending(t => t.Amount)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var sorted = ids.Select(t => t.Value)
            .Where(t => t.From == sender)
            .OrderByDescending(t => t.Amount)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var sorted = ids.Select(t => t.Value)
            .Where(t => t.Status == status)
            .OrderByDescending(t => t.Amount)
            .ToList();

        if (sorted.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return sorted;
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        return ids.Select(t => t.Value)
            .Where(t => t.Status == status && t.Amount <= amount)
            .OrderByDescending(t => t.Amount)
            .ToList();
    }

    public void RemoveTransactionById(int id)
    {
        if (!ids.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        Transaction tr = ids[id];

        ids.Remove(id);

    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var kvp in ids)
        {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    
}

