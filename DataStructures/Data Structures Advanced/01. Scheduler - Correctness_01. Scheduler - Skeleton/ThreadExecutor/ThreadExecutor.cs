using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private Dictionary<int, Task> pool;
    //private Dictionary<int, Task> indexes;
    private List<Task> indexes;
    private Dictionary<Priority, LinkedList<Task>> priorities;
    private OrderedSet<int> consumptions;
    private Dictionary<int, List<Task>> cons;

    public ThreadExecutor()
    {
        this.pool = new Dictionary<int, Task>();
        this.indexes = new List<Task>();
        this.priorities = new Dictionary<Priority, LinkedList<Task>>();
        this.consumptions = new OrderedSet<int>();
        this.cons = new Dictionary<int, List<Task>>();
    }

    public void Execute(Task task)
    {
        if (this.pool.ContainsKey(task.Id))
        {
            throw new ArgumentException();
        }

        this.pool[task.Id] = task;

        int index = indexes.Count;

        task.Index = index;

        this.indexes.Add(task);

        if (!priorities.ContainsKey(task.TaskPriority))
        {
            priorities[task.TaskPriority] = new LinkedList<Task>();
        }

        priorities[task.TaskPriority].AddLast(task);

        this.consumptions.Add(task.Consumption);

        if (!cons.ContainsKey(task.Consumption))
        {
            cons[task.Consumption] = new List<Task>();
        }

        cons[task.Consumption].Add(task);
    }

    public int Count => pool.Count;

    public bool Contains(Task task)
    {
        return this.pool.ContainsKey(task.Id);

    }

    public Task GetById(int id)
    {
        if (!this.pool.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.pool[id];
    }

    public Task GetByIndex(int index)
    {
        if (index < 0 && index >= indexes.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return indexes[index];
    }

    public int Cycle(int cycles)
    {
        if (this.pool.Count == 0)
        {
            throw new InvalidOperationException();
        }

        int count = 0;

        List<Task> result = new List<Task>();

        foreach (var kvp in pool)
        {
            this.cons[kvp.Value.Consumption].Remove(kvp.Value);
            var task = kvp.Value;

            consumptions.Remove(task.Consumption);

            task.Consumption -= cycles;

            consumptions.Add(task.Consumption);

            if (!cons.ContainsKey(task.Consumption))
            {
                cons[task.Consumption] = new List<Task>();
            }

            cons[task.Consumption].Add(task);

            if (task.Consumption <= 0)
            {
                this.cons[task.Consumption].Remove(task);
                result.Add(task);
                count++;
            }
        }

        for (int i = 0; i < result.Count; i++)
        {
            var task = result[i];
            this.pool.Remove(task.Id);
            this.indexes.Remove(task);
            this.priorities[task.TaskPriority].Remove(task);
            
        }

        return count;
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        var range = this.consumptions.Range(lo, inclusive, hi, inclusive);

        var list = new List<Task>();

        foreach (var VARIABLE in range)
        {
            if (cons.ContainsKey(VARIABLE))
            {
                list.AddRange(cons[VARIABLE]);
            }
        }

        return list.OrderBy(t => t.Consumption)
            .ThenByDescending(t => t.TaskPriority);

        //if (inclusive)
        //{
        //    return this.pool
        //        .Select(t => t.Value)
        //        .Where(t => t.Consumption >= lo && t.Consumption <= hi)
        //        .OrderBy(t => t.Consumption)
        //        .ThenByDescending(t => t.TaskPriority)
        //        .ToList();
        //}
        //else
        //{
        //    return this.pool
        //        .Select(t => t.Value)
        //        .Where(t => t.Consumption > lo && t.Consumption < hi)
        //        .OrderBy(t => t.Consumption)
        //        .ThenByDescending(t => t.TaskPriority)
        //        .ToList();
        //}
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        if (!priorities.ContainsKey(type))
        {
            return new List<Task>();
        }
        else
        {
            return priorities[type].OrderByDescending(t => t.Id);
        }
    }

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.pool.ContainsKey(id))
        {
            throw new ArgumentException();
        }


        var task = this.pool[id];

        priorities[task.TaskPriority].Remove(task);

        task.TaskPriority = newPriority;

        if (!priorities.ContainsKey(task.TaskPriority))
        {
            this.priorities[task.TaskPriority] = new LinkedList<Task>();
        }

        this.priorities[task.TaskPriority].AddLast(task);
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        if (!this.priorities.ContainsKey(priority) || this.priorities[priority].Count == 0)
        {
            return new List<Task>();
        }

        return priorities[priority]
            .Where(t => t.Consumption >= lo)
            .OrderByDescending(t => t.Id);
    }

    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var task in this.pool)
        {
            yield return task.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private static Comparison<Task> CompareTask = new Comparison<Task>(
        (Task x, Task y) =>
        {
            int cmp = x.Consumption.CompareTo(y.Consumption);
            if (cmp == 0)
            {
                return y.TaskPriority.CompareTo(x.TaskPriority);
            }
            return cmp;
        });
}
