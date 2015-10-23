using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;

namespace NStripe
{
    public class StripeRequestBase
    {
        public override string ToString()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string queryString = this.ToQueryString();
            Console.WriteLine("Query Formed In {0}ticks", stopwatch.ElapsedTicks);
            return queryString;
        }
    }
}
