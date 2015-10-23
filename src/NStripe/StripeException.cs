using System;
using System.Net;

namespace NStripe
{
    public class StripeException : Exception
    {
        public StripeException(StripeError error)
            : base(error.Message)
        {
            Code = error.Code;
            Param = error.Param;
            Type = error.Type;
        }

        public string Code { get; set; }
        public string Param { get; set; }
        public string Type { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
