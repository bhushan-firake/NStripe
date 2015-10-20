using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeErrors
    {
        public StripeError Error { get; set; }
    }

    public class StripeError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Param { get; set; }
    }
}
