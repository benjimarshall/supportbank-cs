using System;
using Microsoft.VisualBasic.FileIO;

namespace SupportBank
{
    internal class CsvReader
    {
        public static AllPeople ReadCsv(string filename, AllPeople people)
        {
            Program.Logger.Debug($"Starting to parse {filename}");
            using var parser = new TextFieldParser(filename)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] {","}
            };

            while (!parser.EndOfData)
            {
                ProcessCsvTransaction(parser, filename, people);
            }

            Program.Logger.Debug($"{filename} parsed successfully");

            return people;
        }

        private static void ProcessCsvTransaction(TextFieldParser parser, string filename, AllPeople people)
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

            // Fields are: 0: Date, 1: FromAccount, 2: ToAccount, 3: Narrative, 4: Amount
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

            var fromPerson = fields[1];
            var toPerson = fields[2];
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

            people.AddTransaction(new Transaction(date, fromPerson, toPerson, narrative, amount));
        }
    }
}
