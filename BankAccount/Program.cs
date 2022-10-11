using BankAccount.Business;
using System;
using System.IO;

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
                        Deposit();
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

        private static void Deposit()
        {
            Console.WriteLine("Numéro de compte ?");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                Console.WriteLine("Montant ?");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    var result = Bank.Deposit(accountId, amount);
                    switch (result)
                    {
                        case OperationResult.Success:
                            Console.WriteLine("Argent déposé !");
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
    }
}
