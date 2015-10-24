using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NStripe
{
    #region Request

    [Route("/charges", "POST")]
    public class CreateStripeCharge : StripeRequestBase, IResponse<StripeCharge>
    {
        public int Amount { get; set; }
        public string Currency { get; set; }

        //ApplicationFee: Connect only

        public bool? Capture { get; set; }
        public string Description { get; set; }

        //Destination: Connect only

        public Dictionary<string, string> Metadata { get; set; }
        public string ReceiptEmail { get; set; }
        public StripeShippingInfo Shipping { get; set; }
        public string Customer { get; set; }

        //TODO: Card info here
        public string Source { get; set; }
        public string StatementDescriptor { get; set; }
    }

    [Route("/charges/{id}", "GET")]
    public class RetrieveStripeCharge : StripeRequestBase, IResponse<StripeCharge>
    {
        public string Id { get; set; }
    }

    [Route("/charges/{id}", "POST")]
    public class UpdateStripeCharge : StripeRequestBase, IResponse<StripeCharge>
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> FraudDetails { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string ReceiptEmail { get; set; }
        public StripeShippingInfo Shipping { get; set; }
    }

    [Route("/charges/{id}/capture", "POST")]
    public class CaptureStripeCharge : StripeRequestBase, IResponse<StripeCharge>
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        public int? Amount { get; set; }

        //ApplicationFee: Connect only
        public string ReceiptEmail { get; set; }
        public string StatementDescriptor { get; set; }
    }

    [Route("/charges", "GET")]
    public class GetStripeCharges : StripeRequestBase, IResponse<StripeCollection<StripeCharge>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter

        public string Customer { get; set; }
    }

    #endregion Request

    public class StripeCharge : StripeId
    {
        public int Amount { get; set; }
        public int AmountRefunded { get; set; }

        //TODO:Recheck for this expandable property
        public string ApplicationFee { get; set; }

        //TODO:Recheck for this expandable property
        public string BalanceTransaction { get; set; }

        public bool Captured { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }

        //TODO:Expandable property
        public string Customer { get; set; }

        public string Description { get; set; }

        //TODO:Expandable property
        public string Destination { get; set; }

        public StripeDispute Dispute { get; set; }

        public string FailureCode { get; set; }
        public string FailureMessage { get; set; }

        //TODO:Check if should be an enum?
        //public string FraudDetails { get; set; }

        //TODO:Expandablee Property
        public string Invoice { get; set; }

        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public bool Paid { get; set; }
        public string ReceiptEmail { get; set; }
        public string ReceiptNumber { get; set; }
        public bool Refunded { get; set; }

        public StripeCollection<StripeRefund> Refunds { get; set; }

        public StripeShippingInfo Shipping { get; set; }

        public string Career { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string TrackingNumber { get; set; }
        public string StatementDescriptor { get; set; }
        public string Status { get; set; }

        //TODO:Another Expandable property in the same object!
        public string Transfer { get; set; }
    }
}
