using System;

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
            return "Transaction on " + Date.ToShortDateString() + $" of {PoundsToString(Amount)}" +
                   $" from {From.Name}" + $" to {To.Name}" + $" for {Narrative}";
        }

        private static string PoundsToString(double amount)
        {
            return amount.ToString("£0.00;-£0.00;£0.00");
        }
    }
}
