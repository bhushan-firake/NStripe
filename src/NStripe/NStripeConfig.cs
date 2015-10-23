using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class NStripeConfig
    {
        private static string supportedStripeVersion = "2015-10-16";

        static NStripeConfig()
        {
            StripeVersion = supportedStripeVersion;
        }

        public static string StripeVersion { get; private set; }

        public static string ApiKey
        {
            get
            {
                string apiKey = ConfigurationManager.AppSettings["Stripe:ApiKey"];
                if (!string.IsNullOrEmpty(apiKey))
                    return apiKey;
                return "";
            }
            set
            {
                ApiKey = value;
            }
        }
    }
}
