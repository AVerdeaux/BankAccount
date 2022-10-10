using System;
using System.Collections.Generic;

namespace BankAccount.Business
{
    public class Bank : IBank
    {
        private readonly IRegistry mRegistry;
        private IRegistry Registry { get { return mRegistry; } }

        public int CreateAccount(string firstName, string name)
        {
            const int error = -1;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(name))
            {
                return error;
            }
            else if (Registry.GetAccount(firstName, name) != null)
            {
                return error;
            }
            else
            {
                int id = Registry.NextId();
                Registry.StoreNewAccount(new Account(id, firstName, name));
                return id;
            }
        }

        public bool Deposit(int accountId, decimal value)
        {
            if (GetAccount(accountId) != null && value > 0m)
            {
                Registry.StoreOperation(accountId, new Operation(DateTime.Now, value));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Withdraw(int accountId, decimal value)
        {
            if (GetAccount(accountId) != null && value > 0m)
            {
                Registry.StoreOperation(accountId, new Operation(DateTime.Now, -value));
                return true;
            }
            else
            {
                return false;
            }
        }

        public Statement GetStatement(int accountId)
        {
            return null;
        }

        public Account GetAccount(int accountId)
        {
            return Registry.GetAccount(accountId);
        }

        public AccountHistory GetAccountHistory(int accountId)
        {
            return Registry.GetAccountHistory(accountId);
        }

        public Bank(IRegistry registry)
        {
            mRegistry = registry;
        }
    }
}
