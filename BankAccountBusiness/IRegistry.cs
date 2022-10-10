namespace BankAccount.Business
{
    public interface IRegistry
    {
        int NextId();
        void StoreNewAccount(Account account);
        Account GetAccount(int id);
        Account GetAccount(string firstName, string name);
        AccountHistory GetAccountHistory(int id);
        void StoreOperation(int accountId, Operation operation);
    }
}
