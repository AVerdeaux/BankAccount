using System.Collections.Generic;

namespace BankAccount.Business
{
    public class AccountHistory : Account
    {
        public IReadOnlyCollection<OperationHistory> Operations { get; private set; }

        public AccountHistory(Account account, IEnumerable<OperationHistory> operations)
            : base(account.ID, account.FirstName, account.Name)
        {
            Operations = new List<OperationHistory>(operations);
        }
    }
}
