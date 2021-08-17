using System.Linq;

namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    public class PersonCollection : IPersonCollection
    {
        private Dictionary<string, Person> _emails;
        private Dictionary<string, SortedSet<Person>> _domains;
        private Dictionary<string, SortedSet<Person>> _nameAndTown;
        private SortedSet<int> age;
        private Dictionary<int, SortedSet<Person>> _age;

        public PersonCollection()
        {
                this._emails = new Dictionary<string, Person>();
                this._domains = new Dictionary<string, SortedSet<Person>>();
                this._nameAndTown = new Dictionary<string, SortedSet<Person>>();
                this._age = new Dictionary<int, SortedSet<Person>>();
                this.age = new SortedSet<int>();
        }

        public bool AddPerson(string email, string name, int age, string town)
        {
            if (_emails.ContainsKey(email))
            {
                return false;
            }

            var person = new Person()
            {
                Email = email,
                Name = name,
                Age = age,
                Town = town
            };

            this._emails[email] = person;

            var nameAndTown = name + "-||-" + town;

            var domain = email.Split('@')[1];

            if (!this._nameAndTown.ContainsKey(nameAndTown))
            {
                this._nameAndTown[nameAndTown] = new SortedSet<Person>();
            }

            this._nameAndTown[nameAndTown].Add(person);

            if (!this._domains.ContainsKey(domain))
            {
                this._domains[domain] = new SortedSet<Person>();
            }

            this._domains[domain].Add(person);

            this.age.Add(age);

            if (!this._age.ContainsKey(age))
            {
                this._age[age] = new SortedSet<Person>(new CompareByAgeThenByEmail());
            }

            this._age[age].Add(person);

            return true;
        }

        public int Count => this._emails.Count;
        public Person FindPerson(string email)
        {
            if (!this._emails.ContainsKey(email))
            {
                return null;
            }

            return this._emails[email];
        }

        public bool DeletePerson(string email)
        {
            if (!this._emails.ContainsKey(email))
            {
                return false;
            }

            var person = this._emails[email];

            var nameAndTown = person.Name + "-||-" + person.Town;

            var domain = email.Split('@')[1];

            this._emails.Remove(email);
            this._domains[domain].Remove(person);
            this._nameAndTown[nameAndTown].Remove(person);
            this._age[person.Age].Remove(person);

            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            if (!this._domains.ContainsKey(emailDomain))
            {
                return new List<Person>();
            }

            return this._domains[emailDomain];
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var nameAndTown = name + "-||-" + town;

            if (!this._nameAndTown.ContainsKey(nameAndTown))
            {
                return new List<Person>();
            }

            return this._nameAndTown[nameAndTown];
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            var list = new List<Person>();

            var view = this.age.GetViewBetween(startAge, endAge);

            foreach (var age in view)
            {
                if (this._age.ContainsKey(age))
                {
                    list.AddRange(this._age[age]);
                }
            }

            return list;
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            var list = new List<Person>();

            var view = this.age.GetViewBetween(startAge, endAge);

            foreach (var age in view)
            {
                if (this._age.ContainsKey(age))
                {
                    foreach (var VARIABLE in this._age[age])
                    {
                        if (VARIABLE.Town == town)
                        {
                            list.Add(VARIABLE);
                        }
                    }
                }
            }

            return list;
        }
    }

public class CompareByAgeThenByEmail : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        int cmp = x.Age.CompareTo(y.Age);
        if (cmp == 0)
        {
            cmp = x.Email.CompareTo(y.Email);
        }

        return cmp;
    }
}
}
