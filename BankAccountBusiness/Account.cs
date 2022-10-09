using System.Collections.Generic;

namespace BankAccount.Business
{
    public class Account
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Operation> Operations { get; private set; }

        internal Account(int iD, string firstName, string name, IEnumerable<Operation> operations)
        {
            ID = iD;
            FirstName = firstName;
            Name = name;
            Operations = new List<Operation>(operations);
        }
    }
}
