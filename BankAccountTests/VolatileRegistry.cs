using BankAccount.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountTests
{
    class VolatileRegistry : IRegistry
    {
        private readonly List<Account> mAccounts;
        private readonly List<Tuple<int, Operation>> mOperations;

        public void StoreNewAccount(Account account)
        {
            mAccounts.Add(account);
        }

        public Account GetAccount(int id)
        {
            return mAccounts.Single(a => a.ID == id);
        }

        public void StoreOperation(int accountId, Operation operation)
        {
            mOperations.Add(new Tuple<int, Operation>(accountId, operation));
        }

        public VolatileRegistry()
        {
            mAccounts = new List<Account>();
            mOperations = new List<Tuple<int, Operation>>();
        }
    }
}
