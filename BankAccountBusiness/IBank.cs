using System.Collections.Generic;

namespace BankAccount.Business
{
    public interface IBank
    {
        int CreateAccount(string firstName, string name);
        bool Deposit(int accountId, decimal value);
        bool Withdraw(int accountId, decimal value);
        Statement GetStatement(int accountId);
        Account GetAccount(int accountId);
        AccountHistory GetAccountHistory(int accountId);
    }
}
