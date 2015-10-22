using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    public class StripeCollection<T> : StripeId
    {
        public string Url { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}
