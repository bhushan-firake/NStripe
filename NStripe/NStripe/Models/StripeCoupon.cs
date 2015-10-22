using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    //TODO:Recheck
    public class StripeCoupon : StripeId
    {
        public int? PercentOff { get; set; }
        public int? AmountOff { get; set; }
        public string Currency { get; set; }
        public bool Livemode { get; set; }
        public StripeCouponDuration Duration { get; set; }
        public DateTime? RedeemBy { get; set; }
        public int? MaxRedemptions { get; set; }
        public int TimesRedeemed { get; set; }
        public int? DurationInMonths { get; set; }
    }

    public enum StripeCouponDuration
    {
        forever,
        once,
        repeating
    }
}
