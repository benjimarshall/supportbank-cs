using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace SupportBank
{
    internal class CsvReader
    {
        public static Dictionary<string, Person> ReadCsv(string filename)
        {
            var people = new Dictionary<string, Person>();

            using var parser = new TextFieldParser(filename)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] {","}
            };

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();

                // Skip first line
                if (fields[0] == "Date") continue;

                // Fields are: 0: Date, 1: From, 2: To, 3: Narrative, 4: Amount
                var date = DateTime.Parse(fields[0]);
                var fromPerson = FindOrAddPerson(fields[1], people);
                var toPerson = FindOrAddPerson(fields[2], people);
                var narrative = fields[3];
                var amount = double.Parse(fields[4]);

                var transaction = new Transaction(date, fromPerson, toPerson, narrative, amount);

                Console.WriteLine(transaction);

                toPerson.Transactions.Add(transaction);
                fromPerson.Transactions.Add(transaction);

                toPerson.IncreaseBalance(amount);
                fromPerson.DecreaseBalance(amount);
            }

            return people;
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