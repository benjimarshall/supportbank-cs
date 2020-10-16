using System.Collections;
using System.Collections.Generic;

namespace SupportBank
{
    class AllPeople : IEnumerable<Person>
    {
        public Dictionary<string, Person> people = new Dictionary<string, Person>();

        private Person FindOrAddPerson(string name)
        {
            if (people.ContainsKey(name)) return people[name];

            var newPerson = new Person(name);
            people.Add(name, newPerson);
            return newPerson;
        }

        public bool ContainsPerson(string name) => people.ContainsKey(name);

        public Person GetPerson(string name) => people[name];

        public void AddTransaction(Transaction transaction)
        {
            var toPerson = FindOrAddPerson(transaction.ToAccount);
            var fromPerson = FindOrAddPerson(transaction.FromAccount);

            toPerson.Transactions.Add(transaction);
            fromPerson.Transactions.Add(transaction);

            toPerson.IncreaseBalance(transaction.Amount);
            fromPerson.DecreaseBalance(transaction.Amount);
        }

        public IEnumerator<Person> GetEnumerator() => people.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
