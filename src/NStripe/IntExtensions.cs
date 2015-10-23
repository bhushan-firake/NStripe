namespace NStripe
{
    internal static class IntExtensions
    {
        public static string ToStripeDescription(this int statusCode)
        {
            switch (statusCode)
            {
                case 200:
                    return "Everything worked as expected.";

                case 400:
                    return "Often missing a required parameter.";

                case 401:
                    return "No valid API key provided.";

                case 402:
                    return "Parameters were valid but request failed.";

                case 404:
                    return "The requested item doesn't exist.";

                case 500:
                case 502:
                case 503:
                case 504:
                    return "Something went wrong on Stripe's end.";

                default:
                    return "Unclassified error";
            }
        }
    }
}
