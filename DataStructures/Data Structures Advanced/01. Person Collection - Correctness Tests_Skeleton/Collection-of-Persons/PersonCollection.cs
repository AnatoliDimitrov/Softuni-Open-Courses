using System.Linq;
using Wintellect.PowerCollections;

namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    public class PersonCollection : IPersonCollection
    {
        // TODO: define the underlying data structures here ...

        private Dictionary<string, Person> _emails;
        private Dictionary<string, SortedSet<Person>> _domains;
        private Dictionary<string, SortedSet<Person>> _nameAndTown;
        private Dictionary<int, SortedSet<Person>> _byAge;
        private OrderedSet<int> _age;

        public PersonCollection()
        {
            this._emails = new Dictionary<string, Person>();
            this._domains = new Dictionary<string, SortedSet<Person>>();
            this._nameAndTown = new Dictionary<string, SortedSet<Person>>();
            this._byAge = new Dictionary<int, SortedSet<Person>>();
            this._age = new OrderedSet<int>();
        }


        public bool AddPerson(string email, string name, int age, string town)
        {
            if (this._emails.ContainsKey(email))
            {
                return false;
            }

            var person = new Person()
            {
                Name = name,
                Email = email,
                Age = age,
                Town = town
            };

            this._emails[email] = person;

            var currentDomain = email.Split('@')[1];

            if (!_domains.ContainsKey(currentDomain))
            {
                _domains[currentDomain] = new SortedSet<Person>(new CompareByEmail());
            }
            _domains[currentDomain].Add(person);

            var nameAndTown = name + "_|_" + town;

            if (!_nameAndTown.ContainsKey(nameAndTown))
            {
                _nameAndTown[nameAndTown] = new SortedSet<Person>(new CompareByEmail());
            }
            _nameAndTown[nameAndTown].Add(person);

            if (!_byAge.ContainsKey(person.Age))
            {
                _byAge[person.Age] = new SortedSet<Person>(new CompareByEmail());
            }
            _byAge[person.Age].Add(person);

            this._age.Add(person.Age);

            return true;
        }

        public int Count => _emails.Count;
        public Person FindPerson(string email)
        {
            //return this._emails.FirstOrDefault(p => p.Key == email).Value;

            if (this._emails.ContainsKey(email))
            {
                return this._emails[email];
            }

            return null;
        }

        public bool DeletePerson(string email)
        {
            Person person = FindPerson(email);

            if (person == null)
            {
                return false;
            }

            this._emails.Remove(person.Email);

            var currentDomain = email.Split('@')[1];

            this._domains[currentDomain].Remove(person);

            var nameAndTown = person.Name + "_|_" + person.Town;

            this._nameAndTown[nameAndTown].Remove(person);

            this._byAge[person.Age].Remove(person);

            if (_byAge[person.Age].Count == 0)
            {
                this._age.Remove(person.Age);
            }

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
            var nameAndTown = name + "_|_" + town;

            if (!this._nameAndTown.ContainsKey(nameAndTown))
            {
                return new List<Person>();
            }

            return this._nameAndTown[nameAndTown];
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
           var set = new List<Person>();

           var view = _age.Range(startAge, true, endAge, true);

           foreach (var age in view)
           {
               set.AddRange(_byAge[age]);
           }

           return set;
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            var set = new List<Person>();

            var view = _age.Range(startAge, true, endAge, true);

            foreach (var age in view)
            {
                foreach (var person in _byAge[age])
                {
                    if (person.Town == town)
                    {
                        set.Add(person);
                    }
                }
            }

            return set;
        }

        private static readonly Comparison<int> CompareByint = new Comparison<int>(
            (int x, int y) =>
            {
                int cmp = y.CompareTo(x);
                return cmp;
            });
    }

    public class CompareByEmail : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int cmp = x.Email.CompareTo(y.Email);
            return cmp;
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


