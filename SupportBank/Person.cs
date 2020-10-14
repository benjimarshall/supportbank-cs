using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    internal class Person
    {
        public readonly string Name;

        public Person(string name)
        {
            Name = name;
        }

        public double Balance { get; private set; }
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public void IncreaseBalance(double n)
        {
            Balance += n;
        }

        public void DecreaseBalance(double n)
        {
            Balance -= n;
        }

        public string BalanceStatus()
        {
            if (Balance > 0)
                return $"{Name} is owed £{Balance:f2}";
            return $"{Name} owes £{-1 * Balance:f2}";
        }

        public string TransactionSummary()
        {
            var result = new StringBuilder();

            foreach (var transaction in Transactions) Console.WriteLine(transaction.ToString());

            return result.ToString();
        }
    }
}