using System;

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

        public OperationResult Deposit(int accountId, decimal value)
        {
            if (value <= 0m)
            {
                return OperationResult.InvalidAmount;
            }
            else if (GetAccount(accountId) == null)
            {
                return OperationResult.UnknownAccount;
            }
            else
            {
                Registry.StoreOperation(accountId, new Operation(DateTime.Now, value));
                return OperationResult.Success;
            }
        }

        public OperationResult Withdraw(int accountId, decimal value)
        {
            if (value <= 0m)
            {
                return OperationResult.InvalidAmount;
            }
            else if (GetAccount(accountId) == null)
            {
                return OperationResult.UnknownAccount;
            }
            else
            {
                Registry.StoreOperation(accountId, new Operation(DateTime.Now, -value));
                return OperationResult.Success;
            }
        }

        public Statement GetStatement(int accountId)
        {
            var history = GetAccountHistory(accountId);
            if (history == null)
            {
                return null;
            }
            else
            {
                return new Statement(history);
            }
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
