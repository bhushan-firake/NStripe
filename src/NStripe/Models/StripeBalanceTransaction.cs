using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeBalanceTransaction : StripeId
    {
        public int Amount { get; set; }
        public DateTime AvailableOn { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int Fee { get; set; }

        //TODO:Add fee details

        public string Net { get; set; }

        //TODO:Expandable Property
        public string Source { get; set; }

        //TODO: Add sourced_transfers

        public string Status { get; set; }
        public string Type { get; set; }
    }
}
