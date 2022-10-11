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

            while (MainMenu() != MainMenuOption.Quit)
            {
            }
        }

        private static MainMenuOption MainMenu()
        {
            Console.WriteLine("Bienvenue à la Banque Non Sécurisée !");
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("1 : Créer un compte");
            Console.WriteLine("0 : Quitter");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                var option = (MainMenuOption)choice;

                switch (option)
                {
                    case MainMenuOption.AddAccount:
                        AddAccount();
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
    }
}
