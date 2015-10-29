using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/accounts", "POST")]
    public class CreateStripeAccount : StripeRequestBase, IResponse<StripeAccount>
    {
        public string Country { get; set; }
        public string Email { get; set; }
        public bool? Managed { get; set; }
    }

    [Route("/accounts/{accountId}", "GET")]
    public class RetrieveStripeAccount : StripeRequestBase, IResponse<StripeAccount>
    {
        public string AccountId { get; set; }
    }

    [Route("/accounts/{accountId}", "POST")]
    public class UpdateStripeAccount : StripeRequestBase, IResponse<StripeAccount>
    {
        [IgnoreDataMember]
        public string AccountId { get; set; }

        public string BusinessLogo { get; set; }
        public string BusinessName { get; set; }
        public string BusinessPrimaryColor { get; set; }
        public string BusinessUrl { get; set; }

        //Managed Accounts only
        public bool DebitNegativeBalances { get; set; }

        //ToDO:Add decline charge on

        public string DefaultCurrency { get; set; }
        public string Email { get; set; }

        //TODO:External_accounts, legal_entity

        //Managed accounts only
        public string ProductDescription { get; set; }
        public string StatementDescriptor { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string SupportUrl { get; set; }
        public string Timezone { get; set; }

        //TODO:TosAcceptance

        //TODO:TransferSchedule
    }

    [Route("/accounts/{accountId}", "DELETE")]
    public class DeleteStripeAccount : StripeRequestBase, IResponse<StripeAccount>
    {
        public string AccountId { get; set; }
    }

    [Route("/accounts", "GET")]
    public class GetStripeAccounts : StripeRequestBase, IResponse<StripeCollection<StripeAccount>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    #endregion Request

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

        //This is only returned on create call
        public Dictionary<string, string> Keys { get; set; }

        public bool Deleted { get; set; }
    }
}
