using System.Collections.Generic;

namespace SupportBank
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var people = CsvReader.ReadCsv(@"..\..\..\data\Transactions2014.csv");

            var cliInterface = new CliInterface(people);

            cliInterface.RunUserCommandLoop();
        }
    }
}