using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/application_fees/{feeId}")]
    public class RetrieveStripeApplicationFee : StripeRequestBase, IResponse<StripeApplicationFee>
    {
        public string FeeId { get; set; }
    }

    [Route("/application_fees", "GET")]
    public class GetStripeApplicationFees : StripeRequestBase, IResponse<StripeCollection<StripeApplicationFee>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter

        public string Charge { get; set; }
    }

    #endregion Request

    public class StripeApplicationFee : StripeId
    {
        //TODOExpandable Property
        public string Account { get; set; }

        public int Amount { get; set; }
        public int AmountRefunded { get; set; }
        public string Application { get; set; }

        //TODOExpandable Property
        public string BalanceTransaction { get; set; }

        //TODOExpandable Property
        public string Charge { get; set; }

        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public bool Livemode { get; set; }
        public bool Refunded { get; set; }
        public StripeCollection<StripeRefund> Refunds { get; set; }
    }
}
