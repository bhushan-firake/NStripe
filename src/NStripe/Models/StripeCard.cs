using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeCard : StripeId
    {
        //TODO:Expandable && Managed accounts only
        public string Account { get; set; }

        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine1Check { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressState { get; set; }
        public string AddressZip { get; set; }
        public string AddressZipCheck { get; set; }
        public StripeCardType Brand { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }

        //TODO:ExpandableProperty
        public string Customer { get; set; }

        public string CvcCheck { get; set; }
        public bool DefaultForCurrency { get; set; }
        public string DynamicLast4 { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string FingerPrint { get; set; }
        public string Funding { get; set; }
        public string Last4 { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string Recipient { get; set; }
        public string TokenizationMethod { get; set; }
    }

    public static class StripeCardType
    {
        public static const string Visa = "Visa";
        public static const string AmericanExpress = "American Express";
        public static const string MasterCard = "MasterCard";
        public static const string Discover = "Discover";
        public static const string JCB = "JCB";
        public static const string DinersClub = "Diners Club";
        public static const string Unknown = "Unknown";
    }
}
