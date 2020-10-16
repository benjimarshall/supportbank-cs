using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class Transaction
    {
        public readonly double Amount;
        public readonly DateTime Date;
        public readonly Person From;
        public readonly string Narrative;
        public readonly Person To;

        public Transaction(
            DateTime date,
            string fromStr,
            string toStr,
            string narrative,
            double amount,
            Dictionary<string, Person> people
        )
        {
            Date = date;
            From = FindOrAddPerson(fromStr, people);
            To = FindOrAddPerson(toStr, people);
            Narrative = narrative;
            Amount = amount;

            To.Transactions.Add(this);
            From.Transactions.Add(this);

            To.IncreaseBalance(amount);
            From.DecreaseBalance(amount);
        }

        public override string ToString()
        {
            return "Transaction on " + Date.ToShortDateString() + $" of {PoundsToString(Amount)}" +
                   $" from {From.Name}" + $" to {To.Name}" + $" for {Narrative}";
        }

        private static string PoundsToString(double amount)
        {
            return amount.ToString("£0.00;-£0.00;£0.00");
        }

        private static Person FindOrAddPerson(string name, Dictionary<string, Person> people)
        {
            if (people.ContainsKey(name)) return people[name];

            var newPerson = new Person(name);
            people.Add(name, newPerson);
            return newPerson;
        }
    }
}
