using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Instock : IProductStock
{
    List<Product> indexes = new List<Product>();
    Dictionary<string, Product> labels = new Dictionary<string, Product>();
    Dictionary<int, HashSet<Product>> quantities = new Dictionary<int, HashSet<Product>>();
    SortedSet<string> labelsByAB = new SortedSet<string>();
    

    public int Count => indexes.Count;

    public void Add(Product product)
    {
        if (labels.ContainsKey(product.Label))
        {
            return;
        }
        if (!quantities.ContainsKey(product.Quantity))
        {
            quantities[product.Quantity] = new HashSet<Product>();
        }

        quantities[product.Quantity].Add(product);
        labels[product.Label] = product;
        indexes.Add(product);
        labelsByAB.Add(product.Label);
    }

    public bool Contains(Product product)
    {
        return labels.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index >= 0 && index < indexes.Count)
        {
            return indexes[index];
        }

        throw new IndexOutOfRangeException();

        //if (index >= 0 && index < labels.Count)
        //{
        //    foreach (var keyValuePair in labels.Skip(index))
        //    {
        //        return keyValuePair.Value;
        //    }
        //}

        //throw new IndexOutOfRangeException();
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!labels.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var productByLabel = labels[product];

        if (quantities.ContainsKey(productByLabel.Quantity))
        {
            quantities[productByLabel.Quantity].Remove(productByLabel);
        }

        productByLabel.Quantity = quantity;

        if (!quantities.ContainsKey(quantity))
        {
            quantities[quantity] = new HashSet<Product>();
        }

        quantities[quantity].Add(productByLabel);
    }

    public Product FindByLabel(string label)
    {
        if (!labels.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return labels[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count > labelsByAB.Count)
        {
            throw new ArgumentException();
        }

        var list = new List<Product>();

        foreach (var label in labelsByAB)
        {
             list.Add(labels[label]);
             count--;
             if (count == 0)
             {
                 break;
             }
        }

        return list;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return indexes
            .Where(o => o.Price > lo && o.Price <= hi)
            .OrderByDescending(o => o.Price);
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        return indexes
            .Where(o => o.Price == price);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (indexes.Count < count)
        {
            throw new ArgumentException();
        }

        var sorted = indexes
            .OrderByDescending(o => o.Price)
            .Take(count)
            .ToList();

        return sorted;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        //return indexes
        //    .Where(o => o.Quantity == quantity);

        if (!quantities.ContainsKey(quantity))
        {
            return new List<Product>();
        }

        return quantities[quantity].ToList();
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
