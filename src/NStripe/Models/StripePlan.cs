using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    [Route("/plans", "POST")]
    public class CreateStripePlan : StripeRequestBase, IResponse<StripePlan>
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Interval { get; set; }
        public string Name { get; set; }
        public int? IntervalCount { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string StatementDescriptor { get; set; }
        public int? TrialPeriodDays { get; set; }
    }

    [Route("/plans/{id}", "GET")]
    public class RetrieveStripePlan : StripeRequestBase, IResponse<StripePlan>
    {
        public string Id { get; set; }
    }

    [Route("/plans/{id}", "POST")]
    public class UpdateStripePlan : StripeRequestBase, IResponse<StripePlan>
    {
        [IgnoreDataMember]
        public string Id { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string StatementDescriptor { get; set; }
    }

    [Route("/plans/{id}", "DELETE")]
    public class DeleteStripePlan : StripeRequestBase, IResponse<StripePlan>
    {
        public string Id { get; set; }
    }

    [Route("/plans", "GET")]
    public class GetStripePlans : StripeRequestBase, IResponse<StripeCollection<StripePlan>>
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        //TODO:Add Created date filter
    }

    public enum PlanInterval
    {
        Day,
        Week,
        Month,
        Year
    }

    public class StripePlan : StripeId
    {
        public int Amount { get; set; }
        public DateTime Created { get; set; }
        public string Currency { get; set; }
        public PlanInterval Interval { get; set; }
        public int IntervalCount { get; set; }
        public bool Livemode { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public string Name { get; set; }
        public string StatementDescriptor { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
