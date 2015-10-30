using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/balance/history/{transactionId}", "GET")]
    public class RetrievebalanceTransaction : StripeRequestBase, IResponse<StripeBalanceTransaction>
    {
        public string TransactionId { get; set; }
    }

    [Route("/balance/history", "GET")]
    public class GetStripeBalanceHistory : StripeRequestBase, IResponse<StripeCollection<StripeBalanceTransaction>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter
        //TODO:Add available_on filter
        public string Currency { get; set; }
        public string Source { get; set; }
        public string Transfer { get; set; }

        //TODO:Change this type to enum
        public string Type { get; set; }
    }

    #endregion Request

    public class StripeBalanceTransaction : StripeId
    {
        public int Amount { get; set; }
        public DateTime AvailableOn { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int Fee { get; set; }

        //TODO:Add fee details

        public string Net { get; set; }

        //TODO:Expandable Property
        public string Source { get; set; }

        //TODO: Add sourced_transfers

        public string Status { get; set; }
        public string Type { get; set; }
    }
}
