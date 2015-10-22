using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStripe
{
    /// <summary>
    /// Resolves member mappings for a type, lower case underscore  property names.
    /// </summary>
    internal class LowercaseUnderscorePropertyNamesContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CamelCasePropertyNamesContractResolver"/> class.
        /// </summary>
        public LowercaseUnderscorePropertyNamesContractResolver()
            : base()
        {
        }

        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name camel cased.</returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            // apply lowercase underscore on property name
            return propertyName.ToLowercaseUnderscore();
        }
    }
}
