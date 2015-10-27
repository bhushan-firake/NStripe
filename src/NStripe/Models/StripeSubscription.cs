using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NStripe
{
    #region Request

    [Route("/customers/{customerId}/subscriptions", "POST")]
    public class CreateStripeSubscription : StripeRequestBase, IResponse<StripeSubscription>
    {
        [IgnoreDataMember]
        public string CustomerId { get; set; }
        public decimal? ApplicationFeePercent { get; set; }
        public string Coupon { get; set; }
        public string Plan { get; set; }

        //TODO:This can be a card
        public string Source { get; set; }
        public int? Quantity { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? TrialEnd { get; set; }
    }

    [Route("/customers/{customerId}/subscriptions/{subscriptionId}", "GET")]
    public class RetrieveStripeubscription : StripeRequestBase, IResponse<StripeSubscription>
    {
        public string CustomerId { get; set; }
        public string SubscriptionId { get; set; }
    }

    [Route("/customers/{customerId}/subscriptions/{subscriptionId}", "POST")]
    public class UpdateStripeSubscription : StripeRequestBase, IResponse<StripeSubscription>
    {
        [IgnoreDataMember]
        public string CustomerId { get; set; }

        [IgnoreDataMember]
        public string SubscriptionId { get; set; }

        public decimal? ApplicationFeePercent { get; set; }
        public string Coupon { get; set; }
        public string Plan { get; set; }
        public bool? Prorate { get; set; }
        public DateTime? ProrationTime { get; set; }
        public int? Quantity { get; set; }
        public string Source { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? TrialEnd { get; set; }
    }

    [Route("/customers/{customerId}/subscriptions/{subscriptionId}", "DELETE")]
    public class CancelStripeSubscription : StripeRequestBase, IResponse<StripeSubscription>
    {
        [IgnoreDataMember]
        public string CustomerId { get; set; }

        [IgnoreDataMember]
        public string SubscriptionId { get; set; }

        public bool AtPeriodEnd { get; set; }
    }

    [Route("/customers/{customerId}/subscriptions", "GET")]
    public class GetStripeSubscriptions : StripeRequestBase, IResponse<StripeCollection<StripeSubscription>>
    {
        [IgnoreDataMember]
        public string CustomerId { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Customer { get; set; }
    }

    #endregion Request

    public class StripeSubscription : StripeId
    {
        public decimal? ApplicationFeePercent { get; set; }
        public bool? CancelAtPeriodEnd { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }
        public string Customer { get; set; }
        public StripeDiscount Discount { get; set; }
        public DateTime? EndedAt { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public StripePlan Plan { get; set; }
        public int Quantity { get; set; }
        public DateTime? Start { get; set; }
        public StripeSubscriptionStatus Status { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? TrialEnd { get; set; }
        public DateTime? TrialStart { get; set; }
    }

    public enum StripeSubscriptionStatus
    {
        Unknown,
        Trialing,
        Active,
        PastDue,
        Canceled,
        Unpaid
    }
}
