using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class JsonTransaction
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string FromAccount { get; set; }
        public string Narrative { get; set; }
        public string ToAccount { get; set; }
    }
}
