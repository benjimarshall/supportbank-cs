using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SupportBank
{
    internal class CliInterface
    {
        private readonly Dictionary<string, Person> _people;

        public CliInterface(Dictionary<string, Person> people)
        {
            _people = people;
        }

        public void RunUserCommandLoop()
        {
            var listAccountMatcher = new Regex("List (.*)");
            while (true)
            {
                Console.WriteLine("\n\"List All\" to list all balances");
                Console.WriteLine("\"Quit\" to quit");
                Console.WriteLine("\"List [Account]\"");
                Console.WriteLine("Please type an option: ");

                var input = Console.ReadLine();

                if (input == "Quit")
                {
                    break;
                }

                if (input == "List All")
                {
                    Console.WriteLine("\n");
                    PrintBalances();
                }
                else if (listAccountMatcher.IsMatch(input))
                {
                    var match = listAccountMatcher.Match(input);
                    var name = match.Groups[1].ToString();

                    if (_people.ContainsKey(name))
                        Console.WriteLine(_people[name].TransactionSummary());
                    else
                        Console.WriteLine($"Person: {name} wasn't found");
                }
                else
                {
                    Console.WriteLine("Unrecognised input.");
                }
            }
        }

        public void PrintBalances()
        {
            foreach (var person in _people.Values) Console.WriteLine(person.BalanceStatus());
        }

        public void PrintAccountNames()
        {
            foreach (var name in _people.Keys) Console.WriteLine(name);
        }

        public static string PoundsToString(double amount)
        {
            return amount.ToString("£0.00;-£0.00;£0.00");
        }
    }
}