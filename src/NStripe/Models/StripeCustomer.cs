using System;
using System.Collections.Generic;

namespace NStripe
{
    [Route("/customers", "POST")]
    public class CreateStripeCustomer : StripeRequestBase, IResponse<StripeCustomer>
    {
        public int? AccountBalance { get; set; }
        public string Coupon { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int? Quantity { get; set; }
        public StripeShippingInformation Shipping { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? TrialEnd { get; set; }

        //TODO:Source can also be a dictionary
        public string Source { get; set; }
    }

    [Route("/customers/{id}", "GET")]
    public class GetStripeCustomer : StripeRequestBase, IResponse<StripeCustomer>
    {
        public string Id { get; set; }
    }

    public class StripeCustomer : StripeId
    {
        public int AccountBalance { get; set; }
        public DateTime? Created { get; set; }
        public string Currency { get; set; }
        public string DefaultSource { get; set; }
        public bool? Delinquent { get; set; }
        public string Description { get; set; }
        public StripeDiscount Discount { get; set; }
        public string Email { get; set; }
        public bool? Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public StripeShippingInformation Shipping { get; set; }

        //TODO:Add Payment Sources bitcoin receivers/cards

        public StripeCollection<StripeSubscription> Subscriptions { get; set; }
        public string Source { get; set; }
        public bool? Deleted { get; set; }
    }

    public class StripeShippingInformation
    {
        public StripeShippingAddress Address { get; set; }
    }

    public class StripeShippingAddress
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
