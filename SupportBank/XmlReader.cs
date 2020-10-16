using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SupportBank
{
    class XmlReader
    {
        public static AllPeople ReadXml(string filename, AllPeople people)
        {
            Program.Logger.Debug($"Starting to parse {filename}");

            var xml = XElement.Load(filename);

            var jan1900 = new DateTime(1900, 1, 1);

            foreach (var element in xml.Descendants("SupportTransaction"))
            {

                var daysSince1900 = int.Parse(element.Attributes().First().Value);
                var date = jan1900.AddDays(daysSince1900);

                var fromAccount = element.Descendants("From").First().Value;
                var toAccount = element.Descendants("To").First().Value;
                var narrative = element.Descendants("Description").First().Value;
                var amount = double.Parse(element.Descendants("Value").First().Value);

                var transaction = new Transaction(date, fromAccount, toAccount, narrative, amount);

                people.AddTransaction(transaction);
            }

            Program.Logger.Debug($"{filename} parsed successfully");

            return people;
        }
    }
}
