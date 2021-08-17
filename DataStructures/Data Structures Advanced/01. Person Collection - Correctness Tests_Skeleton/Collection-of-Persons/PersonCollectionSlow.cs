using System.Linq;

namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    public class PersonCollectionSlow : IPersonCollection
    {
        // TODO: define the underlying data structures here ...

        private List<Person> _persons;

        public PersonCollectionSlow()
        {
            this._persons = new List<Person>();
        }

        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = new Person()
            {
                Email = email,
                Name = name,
                Age = age,
                Town = town
            };

            foreach (var emailPerson in this._persons)
            {
                if (emailPerson.Email == email)
                {
                    return false;
                }
            }

            _persons.Add(person);
            return true;
        }

        public int Count => this._persons.Count;

        public Person FindPerson(string email)
        {
            return this._persons.FirstOrDefault(p => p.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = FindPerson(email);

            if (person == null)
            {
                return false;
            }

            this._persons
                .Remove(person);

            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            var list = new List<Person>();

            foreach (var person in this._persons)
            {
                var currentDomain = person.Email.Split('@')[1];

                if (currentDomain == emailDomain)
                {
                    list.Add(person);
                }
            }

            return list
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var list = new List<Person>();

            foreach (var person in this._persons)
            {
                if (person.Town == town && person.Name == name)
                {
                    list.Add(person);
                }
            }

            return list
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            var list = new List<Person>();

            foreach (var person in this._persons)
            {
                if (person.Age >= startAge && person.Age <= endAge)
                {
                    list.Add(person);
                }
            }

            return list
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            var list = new List<Person>();

            foreach (var person in this._persons)
            {
                if (person.Age >= startAge && person.Age <= endAge && person.Town == town)
                {
                    list.Add(person);
                }
            }

            return list
                .OrderBy(p => p.Age)
                .ThenBy(p => p.Email);
        }
    }
}
