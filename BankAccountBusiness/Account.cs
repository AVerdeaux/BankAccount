namespace BankAccount.Business
{
    public class Account
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string Name { get; private set; }

        public Account(int id, string firstName, string name)
        {
            ID = id;
            FirstName = firstName;
            Name = name;
        }
    }
}
