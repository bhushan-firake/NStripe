using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
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

        //TODO:Add Stripe Plan

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
