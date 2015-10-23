namespace NStripe
{
    public class StripeCharge : StripeId
    {
        public int Amount { get; set; }

        public string Customer { get; set; }

        public string Currency { get; set; }
    }
}
