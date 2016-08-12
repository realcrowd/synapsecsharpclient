using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class RefreshTokenResponse
    {
        [JsonConverter(typeof(StringUnixDateTimeConverter))]
        public DateTimeOffset ExpiresAt { get; set; }

        public string ExpiresIn { get; set; }

        [JsonProperty("oauth_key")]
        public string OAuthKey { get; set; }

        public string RefreshExpiresIn { get; set; }

        public string RefreshToken { get; set; }
    }
}