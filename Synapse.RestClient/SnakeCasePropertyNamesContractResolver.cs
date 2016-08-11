using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            // convert C# style PascalCase to Synapse's snake_case
            return Regex.Replace(propertyName, @"\B([A-Z])", "_$1").ToLower();
        }
    }
}
