using System;
using System.Linq;

namespace BankAccount.Business
{
    public class Statement
    {
        public DateTime Date { get; private set; }
        public int AccountId { get; private set; }
        public string FirstName { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        internal Statement(AccountHistory account)
        {
            Date = DateTime.Now;
            AccountId = account.ID;
            FirstName = account.FirstName;
            Name = account.Name;
            Balance = account.Operations.Sum(o => o.Amount);
        }
    }
}
