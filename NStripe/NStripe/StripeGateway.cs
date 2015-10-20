using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeGateway
    {
        private const string BaseUrl = "https://api.stripe.com/v1";

        private string apiKey;
        private string publishableKey;
        public ICredentials Credentials { get; set; }
        private string UserAgent { get; set; }

        public StripeGateway(string apiKey, string publishableKey = null)
        {
            this.apiKey = apiKey;
            this.publishableKey = publishableKey;
            Credentials = new NetworkCredential(apiKey, "");
            UserAgent = "NStripe";
        }
    }
}
