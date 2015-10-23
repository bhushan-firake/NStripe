using System;

namespace NStripe
{
    //TODO:Recheck
    public class StripeDiscount : StripeId
    {
        public string Customer { get; set; }
        public StripeCoupon Coupon { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
