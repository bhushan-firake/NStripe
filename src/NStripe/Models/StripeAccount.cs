using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeAccount : StripeId
    {
        public string BusinessName { get; set; }
        public string BusinessPrimaryColor { get; set; }
        public string BusinessUrl { get; set; }
        public bool ChargesEnabled { get; set; }
        public string Country { get; set; }
        public List<string> CurrenciesSupported { get; set; }

        //Managed Accounts only
        public bool DebitNegativeBalances { get; set; }

        //ToDO:Add decline charge on

        public bool AvsFailure { get; set; }
        public bool CvcFailure { get; set; }
        public string DefaultCurrency { get; set; }
        public bool DetailsSubmitted { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        //TODO:External_accounts, legal_entity

        public bool Managed { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        //Managed accounts only
        public string ProductDescription { get; set; }
        public string StatementDescriptor { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string SupportUrl { get; set; }
        public string Timezone { get; set; }

        //TODO:TosAcceptance

        //TODO:TransferSchedule

        public bool TransfersEnabled { get; set; }

        //TODO:Verification
    }
}
