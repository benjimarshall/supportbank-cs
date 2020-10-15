using System.Collections.Generic;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    internal class Program
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            StartLogging();

            var people = new Dictionary<string, Person>();

            CsvReader.ReadCsv(@"..\..\..\data\Transactions2014.csv", people);
            CsvReader.ReadCsv(@"..\..\..\data\DodgyTransactions2015.csv", people);

            var cliInterface = new CliInterface(people);

            cliInterface.RunUserCommandLoop();

            Logger.Debug("Finishing normally...");
        }

        private static void StartLogging()
        {
            var config = new LoggingConfiguration();

            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));

            var consoleTarget = new ConsoleTarget { Layout = @"${level}: ${message}" };
            config.AddTarget("Console", consoleTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, consoleTarget));

            LogManager.Configuration = config;
        }
    }
}