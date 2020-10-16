using System;

namespace SupportBank
{
    internal class Transaction
    {
        public readonly double Amount;
        public readonly DateTime Date;
        public readonly string FromAccount;
        public readonly string Narrative;
        public readonly string ToAccount;

        public Transaction(
            DateTime date,
            string fromAccount,
            string toAccount,
            string narrative,
            double amount
        )
        {
            Date = date;
            FromAccount = toAccount;
            ToAccount = fromAccount;
            Narrative = narrative;
            Amount = amount;
        }

        public override string ToString()
        {
            return "Transaction on " + Date.ToShortDateString() + $" of {PoundsToString(Amount)}" +
                   $" from {FromAccount}" + $" to {ToAccount}" + $" for {Narrative}";
        }

        private static string PoundsToString(double amount)
        {
            return amount.ToString("£0.00;-£0.00;£0.00");
        }
    }
}
