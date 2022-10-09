namespace BankAccount.Business
{
    public class Bank : IBank
    {
        private readonly IRegistry mRegistry;
        private IRegistry Registry { get { return mRegistry; } }

        public int CreateAccount(string firstName, string name)
        {
            return -1;
        }

        public bool Deposit(int accountId, decimal value)
        {
            return false;
        }

        public bool Withdraw(int accountId, decimal value)
        {
            return false;
        }

        public Statement GetStatement(int accountId)
        {
            return null;
        }

        public Account GetAccount(int accountId)
        {
            return null;
        }

        public Bank(IRegistry registry)
        {
            mRegistry = registry;
        }
    }
}
