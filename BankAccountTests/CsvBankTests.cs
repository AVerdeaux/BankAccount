using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BankAccount.Tests
{
    [TestClass()]
    public class CsvBankTests : BankTests<CsvRegistry>
    {
        protected override CsvRegistry CreateRegistry()
        {
            return new CsvRegistry(Path.GetTempFileName());
        }

        protected override void DeleteRegistry(CsvRegistry registry)
        {
            registry.Delete();
        }
    }
}
