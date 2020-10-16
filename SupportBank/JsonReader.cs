using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SupportBank
{
    class JsonReader
    {
        public static AllPeople ReadJson(string filename, AllPeople people)
        {
            Program.Logger.Debug($"Starting to parse {filename}");

            using var file = File.OpenText(filename);

            var serializer = new JsonSerializer();
            var transactionList = (List<Transaction>)serializer.Deserialize(file, typeof(List<Transaction>));

            transactionList.ForEach(people.AddTransaction);

            Program.Logger.Debug($"{filename} parsed successfully");

            return people;
        }
    }
}
