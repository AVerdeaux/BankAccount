using System;

namespace BankAccount.Business
{
    public class Operation
    {
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }

        public Operation(DateTime date, decimal amount)
        {
            Date = date;
            Amount = amount;
        }
    }
}
