using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SupportBank
{
    class JsonReader
    {
        public static void ReadJson(string filename, Dictionary<string, Person> people)
        {
            Program.Logger.Debug($"Starting to parse {filename}");

            using (var file = File.OpenText(filename))
            {
                var serializer = new JsonSerializer();
                var transactionList = (List<JsonTransaction>)serializer.Deserialize(file, typeof(List<JsonTransaction>));

                transactionList.Select(jsonTransaction => new Transaction(
                    jsonTransaction.Date,
                    jsonTransaction.FromAccount,
                    jsonTransaction.ToAccount,
                    jsonTransaction.Narrative,
                    jsonTransaction.Amount,
                    people
                )).ToList();
            }

            Program.Logger.Debug($"{filename} parsed successfully");
        }
    }
}
