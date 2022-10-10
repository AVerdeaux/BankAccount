using BankAccountTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BankAccount.Business.Tests
{
    [TestClass()]
    public class BankTests
    {
        [TestMethod("Create account")]
        public void CreateAccountTest()
        {
            var bank = new Bank(new VolatileRegistry());

            Assert.AreEqual(-1, bank.CreateAccount(null, null), "Account created with null first name / name");
            Assert.AreEqual(-1, bank.CreateAccount("", ""), "Account created with empty first name / name");
            Assert.AreNotEqual(-1, bank.CreateAccount("Alexandre", "TEST"), "First account is not created");
            Assert.AreEqual(-1, bank.CreateAccount("Alexandre", "TEST"), "Can create a second accound for the same person");
        }

        [TestMethod("Get account")]
        public void GetAccountTest()
        {
            var bank = new Bank(new VolatileRegistry());
            int a1 = bank.CreateAccount("Alexandre", "TEST");
            int a2 = bank.CreateAccount("Jean", "TEST");
            int a3 = bank.CreateAccount("Alexandre", "RICHE");

            Assert.IsNull(bank.GetAccount(-1), "Get an account with an id -1");
            Assert.IsNull(bank.GetAccount(0), "Get an account with an id 0");
            Assert.IsNull(bank.GetAccount(100), "Get an account with a not existing id");
            Assert.AreEqual(a1, bank.GetAccount(a1)?.ID, "Can't get the first account");
            Assert.AreEqual(a2, bank.GetAccount(a2)?.ID, "Can't get an account");
            Assert.AreEqual(a3, bank.GetAccount(a3)?.ID, "Can't get the last account");
        }

        [TestMethod("Get account history")]
        public void GetAccountHistoryTest()
        {
            var bank = new Bank(new VolatileRegistry());
            int a1 = bank.CreateAccount("Alexandre", "TEST");
            int a2 = bank.CreateAccount("Jean", "TEST");
            int a3 = bank.CreateAccount("Alexandre", "RICHE");

            Assert.IsNull(bank.GetAccountHistory(-1), "Get an account history with an id -1");
            Assert.IsNull(bank.GetAccountHistory(0), "Get an account history with an id 0");
            Assert.IsNull(bank.GetAccountHistory(100), "Get an account history with a not existing id");
            Assert.AreEqual(a1, bank.GetAccountHistory(a1)?.ID, "Can't get the first account history");
            Assert.AreEqual(a2, bank.GetAccountHistory(a2)?.ID, "Can't get an account history");
            Assert.AreEqual(a3, bank.GetAccountHistory(a3)?.ID, "Can't get the last account history");
        }

        [TestMethod("Get statement")]
        public void GetStatementTest()
        {
            var bank = new Bank(new VolatileRegistry());
            int a1 = bank.CreateAccount("Alexandre", "TEST");

            Assert.IsNotNull(bank.GetStatement(a1), "Can't get the statement of an account");
            Assert.AreEqual(a1, bank.GetStatement(a1)?.AccountId, "Account id on a statement is not right");
            Assert.AreEqual("Alexandre", bank.GetStatement(a1)?.FirstName, "First name on a statement is not right");
            Assert.AreEqual("TEST", bank.GetStatement(a1)?.Name, "First name on a statement is not right");
            Assert.IsTrue((DateTime.Now - bank.GetStatement(a1)?.Date).Value.TotalSeconds < 2, "Date on a statement is not now (less then 2 seconds of delta)");
            Assert.AreEqual(0m, bank.GetStatement(a1)?.Balance, "Balance of a new account is not to 0");
            bank.Deposit(a1, 100m);
            Assert.AreEqual(100m, bank.GetStatement(a1)?.Balance, "Balance of an account after a first deposit is not OK");
            bank.Deposit(a1, 50m);
            Assert.AreEqual(150m, bank.GetStatement(a1)?.Balance, "Balance of an account after a second deposit is not OK");
            bank.Withdraw(a1, 25m);
            Assert.AreEqual(125m, bank.GetStatement(a1)?.Balance, "Balance of an account after a withdrawal is not OK");
            bank.Withdraw(a1, 250m);
            Assert.AreEqual(-125m, bank.GetStatement(a1)?.Balance, "Negative balance of an account after a withdrawal is not OK");
            bank.Deposit(a1, 100m);
            Assert.AreEqual(-25m, bank.GetStatement(a1)?.Balance, "Negative balance of an account after a deposit is not OK");
            bank.Deposit(a1, 50m);
            Assert.AreEqual(25m, bank.GetStatement(a1)?.Balance, "Positive balance of an account after a deposit on a negative balance is not OK");
        }

        [TestMethod("Deposit")]
        public void DepositTest()
        {
            var bank = new Bank(new VolatileRegistry());
            int a1 = bank.CreateAccount("Alexandre", "TEST");

            Assert.AreEqual(OperationResult.Success, bank.Deposit(a1, 100m), "First deposit on an account failed");
            Assert.AreEqual(100m, bank.GetAccountHistory(a1)?.Operations?.Last()?.Amount, "First deposit on an account is not right");

            Assert.AreEqual(OperationResult.Success, bank.Deposit(a1, 50m), "Second deposit on an account failed");
            Assert.AreEqual(50m, bank.GetAccountHistory(a1)?.Operations?.Last()?.Amount, "Second deposit on an account is not right");
            Assert.AreEqual(2, bank.GetAccountHistory(a1)?.Operations?.Count, "Number of deposit on an account is not right");

            Assert.AreEqual(OperationResult.UnknownAccount, bank.Deposit(100, 100m), "A deposit on an unknown account doesn't fail for unknown account");

            Assert.AreEqual(OperationResult.InvalidAmount, bank.Deposit(a1, 0m), "Deposit of 0 on an account doesn't fail for invalid amount");
            Assert.IsFalse(bank.GetAccountHistory(a1).Operations.Any(o => o.Amount == 0m), "Deposit of 0 is in the history of an account");

            Assert.AreEqual(OperationResult.InvalidAmount, bank.Deposit(a1, -10m), "Deposit of a negative amount on an account doesn't fail for invalid amount");
            Assert.IsFalse(bank.GetAccountHistory(a1).Operations.Any(o => o.Amount <= 0m), "Deposit of an invalid amount is in the history of an account");
        }

        [TestMethod("Withdrawal")]
        public void WithdrawTest()
        {
            var bank = new Bank(new VolatileRegistry());
            int a1 = bank.CreateAccount("Alexandre", "TEST");

            Assert.AreEqual(OperationResult.Success, bank.Withdraw(a1, 100m), "First withdrawal on an account failed");
            Assert.AreEqual(-100m, bank.GetAccountHistory(a1)?.Operations?.Last()?.Amount, "First withdrawal on an account is not right");

            Assert.AreEqual(OperationResult.Success, bank.Withdraw(a1, 50m), "Second withdrawal on an account failed");
            Assert.AreEqual(-50m, bank.GetAccountHistory(a1)?.Operations?.Last()?.Amount, "Second withdrawal on an account is not right");
            Assert.AreEqual(2, bank.GetAccountHistory(a1)?.Operations?.Count, "Number of deposit on an account is not right");

            Assert.AreEqual(OperationResult.UnknownAccount, bank.Withdraw(100, 100m), "A withdrawal on an unknown account doesn't fail for unknown account");

            Assert.AreEqual(OperationResult.InvalidAmount, bank.Withdraw(a1, 0m), "Withdrawal of 0 on an account doesn't fail for invalid amount");
            Assert.IsFalse(bank.GetAccountHistory(a1).Operations.Any(o => o.Amount == 0m), "Withdrawal of 0 is in the history of an account");

            Assert.AreEqual(OperationResult.InvalidAmount, bank.Withdraw(a1, -10m), "Withdrawal of a negative amount on an account doesn't fail for invalid amount");
            Assert.IsFalse(bank.GetAccountHistory(a1).Operations.Any(o => o.Amount == -10m), "Withdrawal of an invalid amount is in the history of an account");
        }
    }
}