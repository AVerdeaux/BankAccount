namespace BankAccount.Business
{
    public class OperationHistory : Operation
    {
        public decimal Balance { get; private set; }

        public OperationHistory(decimal previousBalance, Operation operation)
            : base(operation.Date, operation.Amount)
        {
            Balance = previousBalance + operation.Amount;
        }
    }
}
