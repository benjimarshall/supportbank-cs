using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SupportBank
{
    internal class CliInterface
    {
        public static void RunUserCommandLoop(Dictionary<string, Person> people)
        {
            var listAccountMatcher = new Regex("List (.*)");
            while (true)
            {
                Program.Logger.Debug("Presenting main menu");

                Console.WriteLine("\n\"List All\" to list all balances");
                Console.WriteLine("\"Quit\" to quit");
                Console.WriteLine("\"List [Account]\"");
                Console.WriteLine("Please type an option: ");

                var input = Console.ReadLine();

                if (input == "Quit")
                {
                    Program.Logger.Debug("User quit");
                    break;
                }

                if (input == "List All")
                {
                    Program.Logger.Debug("List All");
                    Console.WriteLine("\n");
                    PrintBalances(people);
                }
                else if (listAccountMatcher.IsMatch(input))
                {
                    Program.Logger.Debug("List [Account]");
                    var match = listAccountMatcher.Match(input);
                    var name = match.Groups[1].ToString();

                    if (people.ContainsKey(name))
                    {
                        Program.Logger.Debug($"Listing account {name}");
                        Console.WriteLine(people[name].TransactionSummary());
                    }
                    else
                    {
                        Program.Logger.Debug($"Failed to find account {name}");
                        Console.WriteLine($"Person: {name} wasn't found");
                    }
                }
                else
                {
                    Program.Logger.Debug($"Unrecognised input: {input}");
                    Console.WriteLine("Unrecognised input.");
                }
            }
        }

        private static void PrintBalances(Dictionary<string, Person> people)
        {
            foreach (var person in people.Values) Console.WriteLine(person.BalanceStatus());
        }
    }
}