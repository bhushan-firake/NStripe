using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeRefund : StripeId
    {
        public int Amount { get; set; }

        //TODO:Expandable Property
        public string BalanceTransaction { get; set; }

        //TODO:Expandable Property
        public string Charge { get; set; }

        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Reason { get; set; }
        public string ReceiptNumber { get; set; }
    }
}
