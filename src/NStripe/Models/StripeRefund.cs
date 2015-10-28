using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    #region Request

    [Route("/charges/{chargeId}/refunds", "POST")]
    public class CreateStripeRefund : StripeRequestBase, IResponse<StripeRefund>
    {
        [IgnoreDataMember]
        public string ChargeId { get; set; }

        public int? Amount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Reason { get; set; }

        //Following properties are connect only
        public bool RefundApplicationFee { get; set; }
        public bool ReverseTransfer { get; set; }
    }

    [Route("/refunds/{refundId}", "GET")]
    public class RetrieveStripeRefund : StripeRequestBase, IResponse<StripeRefund>
    {
        public string RefundId { get; set; }
    }

    [Route("/refunds/{refundId}")]
    public class UpdateStripeRefund : StripeRequestBase, IResponse<StripeRefund>
    {
        [IgnoreDataMember]
        public string RefundId { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/refunds", "GET")]
    public class GetStripeRefunds : StripeRequestBase, IResponse<StripeCollection<StripeRefund>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Charge { get; set; }
    }

    #endregion Request

    public class StripeRefund : StripeId
    {
        public int Amount { get; set; }

        //TODO:Expandable Property
        public string BalanceTransaction { get; set; }

        //TODO:Expandable Property
        public string Charge { get; set; }

        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Reason { get; set; }
        public string ReceiptNumber { get; set; }
    }
}
