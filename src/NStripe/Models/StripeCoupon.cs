using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NStripe
{
    #region Request

    [Route("/coupons", "POST")]
    public class CreateStripeCoupon : StripeRequestBase, IResponse<StripeCoupon>
    {
        public string Id { get; set; }
        public string Duration { get; set; }
        public int? AmountOff { get; set; }
        public string Currency { get; set; }
        public int? DurationInMonths { get; set; }
        public int? MaxRedemptions { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int? PercentOff { get; set; }
        public DateTime? RedeemBy { get; set; }
    }

    [Route("/coupons/{couponId}", "GET")]
    public class RetrieveStripeCoupon : StripeRequestBase, IResponse<StripeCoupon>
    {
        public string CouponId { get; set; }
    }

    [Route("/coupons/{couponId}", "POST")]
    public class UpdateStripeCoupon : StripeRequestBase, IResponse<StripeCoupon>
    {
        [IgnoreDataMember]
        public string CouponId { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
    }

    [Route("/coupons/{couponId}", "DELETE")]
    public class DeleteStripeCoupon : StripeRequestBase, IResponse<StripeCoupon>
    {
        public string CouponId { get; set; }
    }

    [Route("/coupons", "GET")]
    public class GetStripeCoupons : StripeRequestBase, IResponse<StripeCollection<StripeCoupon>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter
    }

    #endregion Request

    public class StripeCoupon : StripeId
    {
        public int? AmountOff { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public string Duration { get; set; }
        public int? DurationInMonths { get; set; }
        public bool Livemode { get; set; }
        public int? MaxRedemptions { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        //public StripeCouponDuration Duration { get; set; }
        public int? PercentOff { get; set; }
        public DateTime? RedeemBy { get; set; }
        public int TimesRedeemed { get; set; }
        public bool Valid { get; set; }
    }

    public enum StripeCouponDuration
    {
        Forever,
        Once,
        Repeating
    }
}
