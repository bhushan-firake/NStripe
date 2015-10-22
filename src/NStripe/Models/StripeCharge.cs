using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeCharge : StripeId
    {
        public int Amount { get; set; }

        public string Customer { get; set; }

        public string Currency { get; set; }
    }
}
