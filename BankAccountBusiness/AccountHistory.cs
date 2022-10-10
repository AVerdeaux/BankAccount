using System.Collections.Generic;

namespace BankAccount.Business
{
    public class AccountHistory : Account
    {
        public IReadOnlyCollection<Operation> Operations { get; private set; }

        public AccountHistory(Account account, IEnumerable<Operation> operations)
            : base(account.ID, account.FirstName, account.Name)
        {
            Operations = new List<Operation>(operations);
        }
    }
}
