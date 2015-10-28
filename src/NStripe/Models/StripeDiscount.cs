using System;

namespace NStripe
{
    #region Request

    [Route("/customers/{customerId}/discount", "DELETE")]
    public class DeleteCustomerDiscount : StripeRequestBase, IResponse<StripeDiscount>
    {
        public string CustomerId { get; set; }
    }

    [Route("/customers/{customerId}/subscriptions/{subscriptionId}", "DELETE")]
    public class DeleteSubscriptionDiscount : StripeRequestBase, IResponse<StripeDiscount>
    {
        public string CustomerId { get; set; }
        public string SubscriptionId { get; set; }
    }

    #endregion Request

    public class StripeDiscount : StripeId
    {
        public string Customer { get; set; }
        public StripeCoupon Coupon { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Subscription { get; set; }
    }
}
