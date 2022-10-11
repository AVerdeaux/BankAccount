using BankAccount.Business;
using System;
using System.IO;

namespace BankAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank(new CsvRegistry(Path.Combine(Environment.CurrentDirectory, "Registry.csv")));

            while (MainMenu() != MainMenuOption.Quit)
            {
            }
        }

        private static MainMenuOption MainMenu()
        {
            Console.WriteLine("Bienvenue à la Banque Non Sécurisée !");
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("0 : Quitter");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return (MainMenuOption)choice;
            }
            else
            {
                return MainMenu();
            }
        }
    }
}
