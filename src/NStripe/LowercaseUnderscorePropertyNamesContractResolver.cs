using Newtonsoft.Json.Serialization;

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
