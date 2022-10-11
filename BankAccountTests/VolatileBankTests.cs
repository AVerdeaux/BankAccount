using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAccount.Tests
{
    [TestClass()]
    public class VolatileBankTests : BankTests<VolatileRegistry>
    {
        protected override VolatileRegistry CreateRegistry()
        {
            return new VolatileRegistry();
        }
    }
}
