using System.Configuration;

namespace NStripe
{
    public class NStripeConfig
    {
        private static string _supportedStripeVersion = "2015-10-16";
        private static string _apiKey;

        static NStripeConfig()
        {
            StripeVersion = _supportedStripeVersion;
        }

        public static string StripeVersion { get; private set; }

        public static string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                    _apiKey = ConfigurationManager.AppSettings["Stripe:ApiKey"];
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }
    }
}
