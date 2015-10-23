using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    internal class LowercaseUnderscorePropertyNamesContractResolver : DefaultContractResolver
    {
        public LowercaseUnderscorePropertyNamesContractResolver()
            : base() { }

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowercaseUnderscore();
        }
    }
}
