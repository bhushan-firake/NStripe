using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/accounts/{accountId}/external_accounts", "POST")]
    public class CreateStripeBankAccount : StripeRequestBase, IResponse<StripeBankAccount>
    {
        [IgnoreDataMember]
        public string AccountId { get; set; }

        public bool? DefaultForCurrency { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/accounts/{accountId}/external_accounts/{bankAccountId}", "GET")]
    public class RetrieveStripeBankAccount : StripeRequestBase, IResponse<StripeBankAccount>
    {
        public string AccountId { get; set; }
        public string BankAccountId { get; set; }
    }

    [Route("/accounts/{accountId}/external_accounts/{bankAccountId}", "POST")]
    public class UpdateStripeBankAccount : StripeRequestBase, IResponse<StripeBankAccount>
    {
        [IgnoreDataMember]
        public string AccountId { get; set; }

        [IgnoreDataMember]
        public string BankAccountId { get; set; }

        public bool? DefaultForCurrency { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/accounts/{accountId}/external_accounts/{bankAccountId}", "DELETE")]
    public class DeleteStripeBankAccount : StripeRequestBase, IResponse<StripeBankAccount>
    {
        public string AccountId { get; set; }
        public string BankAccountId { get; set; }
    }

    [Route("/accounts/{accountId}/external_accounts?object=bank_account", "GET")]
    public class GetStripeBankAccounts : StripeRequestBase, IResponse<StripeCollection<StripeBankAccount>>
    {
        [IgnoreDataMember]
        public string AccountId { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    #endregion Request

    public class StripeBankAccount : StripeId
    {
        public string Account { get; set; }
        public string BankName { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public bool DefaultForCurrency { get; set; }
        public string Fingerprint { get; set; }
        public string Last4 { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string RoutingNumber { get; set; }

        //TODO:Enum for this
        public string Status { get; set; }
    }
}
