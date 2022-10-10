namespace BankAccount.Business
{
    public interface IBank
    {
        int CreateAccount(string firstName, string name);
        OperationResult Deposit(int accountId, decimal value);
        OperationResult Withdraw(int accountId, decimal value);
        Statement GetStatement(int accountId);
        Account GetAccount(int accountId);
        AccountHistory GetAccountHistory(int accountId);
    }
}
