namespace BankAccount.Business
{
    public interface IRegistry
    {
        void StoreNewAccount(Account account);
        Account GetAccount(int id);
        void StoreOperation(int accountId, Operation operation);
    }
}
