using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BankAccount.Tests
{
    [TestClass()]
    public class CsvBankTests : BankTests<CsvRegistry>
    {
        protected override CsvRegistry CreateRegistry()
        {
            string filePath = Path.GetTempFileName();
            File.Delete(filePath);
            return new CsvRegistry(filePath);
        }

        protected override void DeleteRegistry(CsvRegistry registry)
        {
            registry.Delete();
        }
    }
}
