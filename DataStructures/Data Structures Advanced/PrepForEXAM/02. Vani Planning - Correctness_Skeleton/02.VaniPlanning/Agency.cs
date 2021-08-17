using System.Linq;
using Wintellect.PowerCollections;

namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> numbers;

        public Agency()
        {
            numbers = new Dictionary<string, Invoice>();
        }

        public void Create(Invoice invoice)
        {
            if (numbers.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            this.numbers[invoice.SerialNumber] = invoice;
        }

        public bool Contains(string number)
        {
            return numbers.ContainsKey(number);
        }

        public int Count()
        {
            return numbers.Count;
        }

        public void PayInvoice(DateTime due)
        {
            var counter = 0;

            foreach (var kvp in numbers)
            {
                if (kvp.Value.DueDate == due)
                {
                    kvp.Value.Subtotal = 0;
                    counter++;
                }
            }

            if (counter == 0)
            {
                throw new ArgumentException();
            }
        }

        public void ThrowInvoice(string number)
        {
            if (!numbers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            numbers.Remove(number);
        }

        public void ThrowPayed()
        {
            var list = new List<Invoice>();
            foreach (var kvp in numbers)
            {
                if (kvp.Value.Subtotal == 0)
                {
                    list.Add(kvp.Value);
                }
            }

            foreach (var invoice in list)
            {
                numbers.Remove(invoice.SerialNumber);
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return numbers
                .Select(k => k.Value)
                .Where(i => i.IssueDate >= start && i.IssueDate <= end)
                .OrderBy(i => i.IssueDate)
                .ThenBy(i => i.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var list = new List<Invoice>();

            foreach (var kvp in numbers)
            {
                if (kvp.Key.Contains(serialNumber))
                {
                    list.Add(kvp.Value);
                }
            }

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            return list.OrderByDescending(i => i.SerialNumber);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {

            var list = new List<Invoice>();

            foreach (var kvp in numbers)
            {
                if (kvp.Value.DueDate > start && kvp.Value.DueDate < end)
                {
                    list.Add(kvp.Value);
                }
            }

            foreach (var invoice in list)
            {
                numbers.Remove(invoice.SerialNumber);
            }

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {

            return numbers
                .Select(k => k.Value)
                .Where(i => i.Department == department)
                .OrderByDescending(i => i.Subtotal)
                .ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {

            return numbers
                .Select(k => k.Value)
                .Where(i => i.CompanyName == company)
                .OrderByDescending(i => i.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            int counter = 0;

            foreach (var kvp in numbers)
            {
                if (kvp.Value.DueDate == dueDate)
                {
                    kvp.Value.DueDate = kvp.Value.DueDate.AddDays(days);
                    counter++;
                }
            }

            if (counter == 0)
            {
                throw  new ArgumentException();
            }
        }
    }
}
