using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeHeaders
    {
        public const string IdempotencyKey = "Idempotency-Key";
        public const string RequestId = "Request-Id";
        public const string StripeVersion = "Stripe-Version";
    }
}
