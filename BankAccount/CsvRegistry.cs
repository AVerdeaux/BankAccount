using BankAccount.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankAccount
{
    public class CsvRegistry : IRegistry
    {
        private readonly string mCsvFilePath;
        private const char mSeparator = ';';

        private static class ColumnIndex
        {
            public const int Id = 0;
            public const int FirstName = 1;
            public const int Name = 2;
            public const int Date = 3;
            public const int Amount = 4;
        }

        public int NextId()
        {
            var ids = GetLines()
                .Select(line => int.Parse(line.Split(mSeparator)[ColumnIndex.Id]));
            int lastAccountId = ids.Any() ? ids.Max() : 0;
            return lastAccountId + 1;
        }

        public void StoreNewAccount(Account account)
        {
            File.AppendAllLines(
                mCsvFilePath,
                new[] { string.Join(mSeparator.ToString(), account.ID.ToString(), account.FirstName, account.Name) });
        }

        public Account GetAccount(int id)
        {
            return GetLines()
                .Select(line => line.Split(mSeparator))
                .Where(details => details.Length == 3)
                .Select(details => new Account(int.Parse(details[ColumnIndex.Id]), details[ColumnIndex.FirstName], details[ColumnIndex.Name]))
                .SingleOrDefault(a => a.ID == id);
        }

        public Account GetAccount(string firstName, string name)
        {
            return GetLines()
                .Select(line => line.Split(mSeparator))
                .Where(details => details.Length == 3)
                .Where(details => details[ColumnIndex.FirstName] == firstName && details[ColumnIndex.Name] == name)
                .Select(details => new Account(int.Parse(details[ColumnIndex.Id]), details[ColumnIndex.FirstName], details[ColumnIndex.Name]))
                .SingleOrDefault();
        }

        public AccountHistory GetAccountHistory(int id)
        {
            var account = GetAccount(id);
            if (account == null)
            {
                return null;
            }
            else
            {
                var operations = GetLines()
                    .Select(line => line.Split(mSeparator))
                    .Where(details => details.Length == 5)
                    .Where(details => int.Parse(details[ColumnIndex.Id]) == id)
                    .Select(details => new Operation(ToDate(details[ColumnIndex.Date]), decimal.Parse(details[ColumnIndex.Amount])));
                var history = new List<OperationHistory>();
                foreach (var operation in operations)
                {
                    history.Add(new OperationHistory(history.Any() ? history.Last().Balance : 0m, operation));
                }
                return new AccountHistory(account, new List<OperationHistory>(history));
            }
        }
        private static DateTime ToDate(string storedDate)
        {
            return new DateTime(
                int.Parse(storedDate.Substring(0, 4)),
                int.Parse(storedDate.Substring(5, 2)),
                int.Parse(storedDate.Substring(8, 2)),
                int.Parse(storedDate.Substring(11, 2)),
                int.Parse(storedDate.Substring(14, 2)),
                int.Parse(storedDate.Substring(17, 2)));
        }

        public void StoreOperation(int accountId, Operation operation)
        {
            var values = new string[] {
                accountId.ToString(),
                string.Empty,
                string.Empty,
                operation.Date.ToString("yyyy-MM-dd hh:mm:ss"),
                operation.Amount.ToString() };
            File.AppendAllLines(
                mCsvFilePath,
                new[] { string.Join(mSeparator.ToString(), values) });
        }

        public CsvRegistry(string csvFilePath)
        {
            mCsvFilePath = csvFilePath;
        }


        private IEnumerable<string> GetLines()
        {
            if (File.Exists(mCsvFilePath))
            {
                return File.ReadAllLines(mCsvFilePath);
            }
            else
            {
                return new string[] { };
            }
        }

        public void Delete()
        {
            System.IO.File.Delete(mCsvFilePath);
        }
    }
}
