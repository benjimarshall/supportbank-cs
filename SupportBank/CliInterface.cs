﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SupportBank
{
    internal class CliInterface
    {
        public static void RunUserCommandLoop(Dictionary<string, Person> people)
        {
            var listAccountMatcher = new Regex("List (.*)");
            var inputFileMatcher = new Regex(@"Input File (.*)");

            while (true)
            {
                Program.Logger.Debug("Presenting main menu");

                Console.WriteLine("\n\"List All\" to list all balances");
                Console.WriteLine("\"Quit\" to quit");
                Console.WriteLine("\"List [Account]\"");
                Console.WriteLine("\"Input File [filename]\"");
                Console.WriteLine("Please type an option: ");

                var input = Console.ReadLine();

                var listAccountMatch = listAccountMatcher.Match(input);
                var inputFileMatch = inputFileMatcher.Match(input);


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
                else if (listAccountMatch.Success)
                {
                    Program.Logger.Debug("List [Account]");
                    var name = listAccountMatch.Groups[1].ToString();

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
                else if (inputFileMatch.Success)
                {
                    Program.Logger.Debug("Input File [filename]");
                    ReadFromFile(inputFileMatch.Groups[1].ToString(), people);
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

        private static void ReadFromFile(string filename, Dictionary<string, Person> people)
        {
            var fileTypeMatcher = new Regex(@".*\.((csv)|(json))");

            if (fileTypeMatcher.IsMatch(filename))
            {
                if (fileTypeMatcher.Match(filename).Groups[1].ToString() == "csv")
                {
                    Program.Logger.Debug($"Working on CSV: {filename}");
                    CsvReader.ReadCsv(filename, people);
                }
                else if (fileTypeMatcher.Match(filename).Groups[1].ToString() == "json")
                {
                    Program.Logger.Debug($"Working on JSON: {filename}");
                    JsonReader.ReadJson(filename, people);
                }
                else
                {
                    Program.Logger.Fatal("Major error parsing file name, matched with file extension "
                                         + fileTypeMatcher.Match(filename).Groups[1].ToString());
                }
            }
            else
            {
                Program.Logger.Debug($"Unsupported file type for file: {filename}");
                Console.WriteLine("Unsupported file type.");
            }
        }
    }
}
