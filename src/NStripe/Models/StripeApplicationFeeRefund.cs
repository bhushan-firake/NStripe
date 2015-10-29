using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/application_fees/{feeId}/refunds", "POST")]
    public class CreateStripeApplicationFeeRefund : StripeRequestBase, IResponse<StripeApplicationFeeRefund>
    {
        [IgnoreDataMember]
        public string FeeId { get; set; }

        public int? Amount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/application_fees/{feeId}/refunds/{refundId}", "GET")]
    public class RetrieveStripeApplicationFeeRefund : StripeRequestBase, IResponse<StripeApplicationFeeRefund>
    {
        public string FeeId { get; set; }
        public string RefundId { get; set; }
    }

    [Route("/application_fees/{feeId}/refunds/{refundId}", "POST")]
    public class UpdateStripeApplicationFeeRefund : StripeRequestBase, IResponse<StripeApplicationFeeRefund>
    {
        [IgnoreDataMember]
        public string FeeId { get; set; }

        [IgnoreDataMember]
        public string RefundId { get; set; }

        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/application_fees/{feeId}/refunds", "GET")]
    public class GetStripeApplicationFeeRefunds : StripeRequestBase, IResponse<StripeCollection<StripeApplicationFeeRefund>>
    {
        public string FeeId { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

    #endregion Request

    public class StripeApplicationFeeRefund : StripeId
    {
        public int Amount { get; set; }

        //TODO:Expandable Property
        public string BalanceTransaction { get; set; }

        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Fee { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }
}
