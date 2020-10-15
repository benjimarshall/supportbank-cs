using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace SupportBank
{
    internal class CsvReader
    {
        private Dictionary<string, Person> people;

        public CsvReader(Dictionary<string, Person> people)
        {
            this.people = people;
        }

        public void ReadCsv(string filename)
        {
            Program.Logger.Debug($"Starting to parse {filename}");
            using var parser = new TextFieldParser(filename)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] {","}
            };

            while (!parser.EndOfData)
            {

                ProcessTransaction(parser, filename);
            }

            Program.Logger.Debug($"{filename} parsed successfully");
        }

        private void ProcessTransaction(TextFieldParser parser, string filename)
        {
            var fields = parser.ReadFields();

            Program.Logger.Debug($"Parsing line {parser.LineNumber}: {string.Join(",", fields)}");

            // Skip first line
            if (fields[0] == "Date") return;

            if (fields.Length < 5)
            {
                Program.Logger.Error($"Not enough fields for transaction on {parser.LineNumber} of {filename}");
                return;
            }

            // Fields are: 0: Date, 1: From, 2: To, 3: Narrative, 4: Amount
            DateTime date;
            try
            {
                date = DateTime.Parse(fields[0]);
            }
            catch (FormatException e)
            {
                Program.Logger.Error(
                    $"Could not parse date \"{fields[0]}\" for transaction on line {parser.LineNumber} in {filename}"
                );
                return;
            }

            var fromPerson = FindOrAddPerson(fields[1], people);
            var toPerson = FindOrAddPerson(fields[2], people);
            var narrative = fields[3];

            double amount;
            try
            {
                amount = double.Parse(fields[4]);
            }
            catch (FormatException e)
            {
                Program.Logger.Error(
                    $"Could not parse amount \"{fields[4]}\" for transaction on line {parser.LineNumber} in {filename}"
                );
                return;
            }

            var transaction = new Transaction(date, fromPerson, toPerson, narrative, amount);

            toPerson.Transactions.Add(transaction);
            fromPerson.Transactions.Add(transaction);

            toPerson.IncreaseBalance(amount);
            fromPerson.DecreaseBalance(amount);

        }

        private static Person FindOrAddPerson(string name, Dictionary<string, Person> people)
        {
            if (people.ContainsKey(name)) return people[name];

            var newPerson = new Person(name);
            people.Add(name, newPerson);
            return newPerson;
        }
    }
}