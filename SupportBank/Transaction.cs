using System;
using System.Text;

namespace SupportBank
{
    internal class Transaction
    {
        public readonly double Amount;
        public readonly DateTime Date;
        public readonly Person From;
        public readonly string Narrative;
        public readonly Person To;

        public Transaction(DateTime date, Person from, Person to, string narrative, double amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("Transaction on ")
                .Append(Date.ToShortDateString())
                .Append($" of £{CliInterface.PoundsToString(Amount)}")
                .Append($" from {From.Name}")
                .Append($" to {To.Name}")
                .Append($" for {Narrative}");

            return result.ToString();
        }
    }
}