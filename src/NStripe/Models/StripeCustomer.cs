using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NStripe
{
    #region Request

    [Route("/customers", "POST")]
    public class CreateStripeCustomer : StripeRequestBase, IResponse<StripeCustomer>
    {
        public int? AccountBalance { get; set; }
        public string Coupon { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int? Quantity { get; set; }
        public StripeShippingInfo Shipping { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? TrialEnd { get; set; }

        //TODO:Source can also be a dictionary
        public string Source { get; set; }
    }

    [Route("/customers/{id}", "GET")]
    public class RetrieveStripeCustomer : StripeRequestBase, IResponse<StripeCustomer>
    {
        public string Id { get; set; }
    }

    [Route("/customers/{id}", "POST")]
    public class UpdateStripeCustomer : StripeRequestBase, IResponse<StripeCustomer>
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        public int? AccountBalance { get; set; }
        public string Coupon { get; set; }
        public string DefaultSource { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public StripeShippingInfo Shipping { get; set; }

        //TODO:Source can also be a dictionary
        public string Source { get; set; }
    }

    [Route("/customers/{id}", "DELETE")]
    public class DeleteStripeCustomer : StripeRequestBase, IResponse<StripeDeletedReference>
    {
        public string Id { get; set; }
    }

    [Route("/customers", "GET")]
    public class GetStripeCustomers : IResponse<StripeCollection<StripeCustomer>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter
    }

    #endregion Request

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
        public StripeShippingInfo Shipping { get; set; }

        //TODO:Add Payment Sources bitcoin receivers/cards

        public StripeCollection<StripeSubscription> Subscriptions { get; set; }
        public string Source { get; set; }
        public bool? Deleted { get; set; }
    }

    public class StripeShippingInfo
    {
        public StripeShippingAddress Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class StripeShippingAddress
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
    }

    public class StripeDeletedReference
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
    }
}
