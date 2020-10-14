using System.Collections.Generic;

namespace SupportBank
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var people = new Dictionary<string, Person>();

            CsvReader.ReadCsv(@"..\..\..\data\Transactions2014.csv", people);

            var cliInterface = new CliInterface(people);

            cliInterface.RunUserCommandLoop();
        }
    }
}