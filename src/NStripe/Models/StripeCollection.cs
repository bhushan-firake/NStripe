using System.Collections.Generic;

namespace NStripe
{
    public class StripeCollection<T> : StripeId
    {
        public string Url { get; set; }
        public bool HasMore { get; set; }
        public List<T> Data { get; set; }
    }
}
