using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripePlan : StripeId
    {
        public int Amount { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public int IntervalCount { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string StatementDescriptor { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
