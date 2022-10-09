﻿using BankAccount.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountTests
{
    class VolatileRegistry : IRegistry
    {
        private readonly List<Account> mAccounts;
        private readonly List<Tuple<int, Operation>> mOperations;

        public int NextId()
        {
            if (mAccounts.Any())
            {
                return mAccounts.Max(a => a.ID) + 1;
            }
            else
            {
                return 1;
            }
        }

        public void StoreNewAccount(Account account)
        {
            mAccounts.Add(account);
        }

        public Account GetAccount(int id)
        {
            return mAccounts.SingleOrDefault(a => a.ID == id);
        }

        public Account GetAccount(string firstName, string name)
        {
            return mAccounts.SingleOrDefault(a => a.FirstName == firstName && a.Name == name);
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