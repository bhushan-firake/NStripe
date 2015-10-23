﻿using System.Configuration;

namespace NStripe
{
    public class NStripeConfig
    {
        private static string supportedStripeVersion = "2015-10-16";
        private static string apiKey;

        static NStripeConfig()
        {
            StripeVersion = supportedStripeVersion;
        }

        public static string StripeVersion { get; private set; }

        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(apiKey))
                    apiKey = ConfigurationManager.AppSettings["Stripe:ApiKey"];
                return apiKey;
            }
            set
            {
                apiKey = value;
            }
        }
    }
}
