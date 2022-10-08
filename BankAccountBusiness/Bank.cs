namespace BankAccount.Business
{
    public class Bank : IBank
    {
        public int CreateAccount(string firstName, string name)
        {
            return -1;
        }

        public bool Deposit(int accountId, double value)
        {
            return false;
        }

        public bool Withdraw(int accountId, double value)
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
    }
}
