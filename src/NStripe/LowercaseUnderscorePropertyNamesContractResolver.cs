using Newtonsoft.Json.Serialization;

namespace NStripe
{
    internal class LowercaseUnderscorePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowercaseUnderscore();
        }
    }
}
