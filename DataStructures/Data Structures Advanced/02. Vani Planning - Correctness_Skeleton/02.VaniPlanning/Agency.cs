using System.Linq;
using Wintellect.PowerCollections;

namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> Numbers;
        private Dictionary<DateTime, Bag<Invoice>> DueDates;
        private List<Invoice> Paied;

        public Agency()
        {
            this.Numbers = new Dictionary<string, Invoice>();
            this.DueDates = new Dictionary<DateTime, Bag<Invoice>>();
            this.Paied = new List<Invoice>();
        }

        public void Create(Invoice invoice)
        {
            if (this.Numbers.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }
            this.Numbers.Add(invoice.SerialNumber, invoice);

            if (!DueDates.ContainsKey(invoice.DueDate))
            {
                DueDates[invoice.DueDate] = new Bag<Invoice>();
            }
            this.DueDates[invoice.DueDate].Add(invoice);

            if (invoice.Subtotal == 0)
            {
                Paied.Add(invoice);
            }
        }

        public void PayInvoice(DateTime due)
        {
            if (!DueDates.ContainsKey(due) || DueDates[due].Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in DueDates[due])
            {
                invoice.Subtotal = 0;
                Paied.Add(invoice);
            }
        }

        public void ThrowInvoice(string number)
        {
            if (!Numbers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var invoice = Numbers[number];

            Numbers.Remove(number);
            DueDates[invoice.DueDate].Remove(invoice);
        }

        public void ThrowPayed()
        {
            foreach (var invoice in Paied)
            {
                Numbers.Remove(invoice.SerialNumber);
                DueDates[invoice.DueDate].Remove(invoice);
            }
        }

        public int Count()
        {
            return this.Numbers.Count;
        }

        public bool Contains(string number)
        {
            return this.Numbers.ContainsKey(number);
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            var list = new List<Invoice>();

            foreach (var kvp in Numbers)
            {
                if (kvp.Value.IssueDate >= start && kvp.Value.IssueDate <= end)
                {
                    list.Add(kvp.Value);
                }
            }

            return list
                .OrderBy(c => c.IssueDate)
                .ThenBy(c => c.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var set = new List<Invoice>();

            foreach (var kvp in this.Numbers)
            {
                if (kvp.Key.Contains(serialNumber))
                {
                    set.Add(kvp.Value);
                }
            }

            if (set.Count == 0)
            {
                throw new ArgumentException();
            }
            return set.OrderByDescending(c => c.SerialNumber);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var list = new List<Invoice>();

            foreach (var kvp in DueDates)
            {
                if (kvp.Key > start && kvp.Key < end)
                {
                    foreach (var invoice in kvp.Value)
                    {
                        list.Add(invoice);
                    }
                }
            }

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < list.Count; i++)
            {
                var invoice = list[i];

                this.Numbers.Remove(invoice.SerialNumber);
                this.DueDates[invoice.DueDate].Remove(invoice);
                this.Paied.Remove(invoice);
            }

            return list;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            var list = new List<Invoice>();

            foreach (var kvp in Numbers)
            {
                if (kvp.Value.Department == department)
                {
                    list.Add(kvp.Value);
                }
            }

            if (list.Count == 0)
            {
                return list;
            }

            return list
                .OrderByDescending(c => c.Subtotal)
                .ThenBy(c => c.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {

            var list = new List<Invoice>();

            foreach (var kvp in Numbers)
            {
                if (kvp.Value.CompanyName == company)
                {
                    list.Add(kvp.Value);
                }
            }

            if (list.Count == 0)
            {
                return list;
            }

            return list
                .OrderByDescending(c => c.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            if (!this.DueDates.ContainsKey(dueDate) || DueDates[dueDate].Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in this.DueDates[dueDate])
            {
                invoice.DueDate = invoice.DueDate.AddDays(days);
            }
        }
    }
}
