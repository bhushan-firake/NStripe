using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeDispute : StripeId
    {
        public int Amount { get; set; }
        public List<StripeBalanceTransaction> BalanceTransactions { get; set; }

        //TODO:Expandable property
        public string Charge { get; set; }

        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public StripeDisputeEvidence Evidence { get; set; }
        public bool IsChargeRefundable { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }

    public class StripeDisputeEvidence
    {
        public string AccessActivityLog { get; set; }
        public string BillingAddress { get; set; }
        public string CancellationPolicy { get; set; }
        public string CancellationPolicyDisclosure { get; set; }
        public string CancellationRebuttal { get; set; }
        public string CustomerCommunication { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPurchaseIp { get; set; }
        public string CustomerSignature { get; set; }
        public string DuplicateChargeDocumentation { get; set; }
        public string DuplicateChargeExplanation { get; set; }
        public string DuplicateChargeId { get; set; }
        public string ProductDescription { get; set; }
        public string Receipt { get; set; }
        public string RefundPolicy { get; set; }
        public string RefundPolicyDisclosure { get; set; }
        public string RefundRefusalExplanation { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceDocumentation { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCareer { get; set; }
        public string ShippingDate { get; set; }
        public string ShippingDocumentation { get; set; }
        public string ShippingTrackingNumber { get; set; }
        public string UncategorizedFile { get; set; }
        public string UncategorizedText { get; set; }
    }
}
