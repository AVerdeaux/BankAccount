using System.Collections.Generic;

namespace BankAccount.Business
{
    public interface IBank
    {
        int CreateAccount(string firstName, string name);
        bool Deposit(int accountId, double value);
        bool Withdraw(int accountId, double value);
        Statement GetStatement(int accountId);
        Account GetAccount(int accountId);
    }
}
