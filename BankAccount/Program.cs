using BankAccount.Business;
using System;
using System.IO;
using System.Linq;

namespace BankAccount
{
    class Program
    {
        private static Bank Bank { get; set; }

        static void Main(string[] args)
        {
            Bank = new Bank(new CsvRegistry(Path.Combine(Environment.CurrentDirectory, "Registry.csv")));

            Console.WriteLine("Bienvenue à la Banque Non Sécurisée !");

            while (MainMenu() != MainMenuOption.Quit)
            {
                Console.WriteLine();
            }
        }

        private static MainMenuOption MainMenu()
        {
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("1 : Créer un compte");
            Console.WriteLine("2 : Déposer de l'argent");
            Console.WriteLine("3 : Retirer de l'argent");
            Console.WriteLine("4 : Situation d'un compte");
            Console.WriteLine("5 : Historique d'un compte");
            Console.WriteLine("0 : Quitter");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                var option = (MainMenuOption)choice;

                switch (option)
                {
                    case MainMenuOption.AddAccount:
                        AddAccount();
                        break;
                    case MainMenuOption.Deposit:
                        Operation("Argent déposé !", Bank.Deposit);
                        break;
                    case MainMenuOption.Withdraw:
                        Operation("Retrait effectué !", Bank.Withdraw);
                        break;
                    case MainMenuOption.Statement:
                        DisplayStatement();
                        break;
                    case MainMenuOption.History:
                        DisplayHistory();
                        break;
                    default:
                        break;
                }

                return option;
            }
            else
            {
                return MainMenu();
            }
        }

        private static void AddAccount()
        {
            Console.WriteLine("Prénom ?");
            string firstName = Console.ReadLine();

            Console.WriteLine("Nom ?");
            string name = Console.ReadLine();

            var accountId = Bank.CreateAccount(firstName, name);
            if (accountId > 0)
            {
                Console.WriteLine(string.Format("Bravo ! Votre numéro de compte est : {0}", accountId));
            }
            else
            {
                Console.WriteLine("Compte non créé, prénom et nom mal renseignés ou un compte existe déjà pour l'individu");
            }
        }

        private static void Operation(string successMessage, Func<int, decimal, OperationResult> operation)
        {
            Console.WriteLine("Numéro de compte ?");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Montant ?");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var result = operation(accountId, amount);
                    switch (result)
                    {
                        case OperationResult.Success:
                            Console.WriteLine(successMessage);
                            break;
                        case OperationResult.UnknownAccount:
                            Console.WriteLine("Compte inconnu !");
                            break;
                        case OperationResult.InvalidAmount:
                            Console.WriteLine("Montant strictement positif svp !");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Montant invalide !");
                }
            }
            else
            {
                Console.WriteLine("Numéro de compte invalide !");
            }
        }

        private static void DisplayStatement()
        {
            Console.WriteLine("Numéro de compte ?");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                var statement = Bank.GetStatement(accountId);
                if (statement != null)
                {
                    Console.WriteLine(
                        string.Format("à la date du {0:G}, le compte de {1} {2} est à {3}.",
                        statement.Date,
                        statement.FirstName,
                        statement.Name,
                        statement.Balance));
                }
                else
                {
                    Console.WriteLine("Situation non récupérée. Numéro de compte invalide ?");
                }
            }
            else
            {
                Console.WriteLine("Numéro de compte invalide !");
            }
        }

        private static void DisplayHistory()
        {
            Console.WriteLine("Numéro de compte ?");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                var history = Bank.GetAccountHistory(accountId);
                if (history != null)
                {

                    if (history.Operations.Any())
                    {
                        Console.WriteLine(
                            string.Format("Depuis le {0:G}, le compte de {1} {2} a l'historique suivant :",
                            history.Operations.Last().Date,
                            history.FirstName,
                            history.Name));
                        var lines = history.Operations.Select(
                            o => string.Format("{0:G} : {1}{2} => Solde à {3}", o.Date, o.Amount > 0 ? "+" : "", o.Amount, o.Balance));
                        foreach (var line in lines)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            string.Format("Le compte de {0} {1} n'a encore aucun historique.",
                            history.FirstName,
                            history.Name));
                    }
                }
                else
                {
                    Console.WriteLine("Historique non récupéré. Numéro de compte invalide ?");
                }
            }
            else
            {
                Console.WriteLine("Numéro de compte invalide !");
            }
        }
    }
}
