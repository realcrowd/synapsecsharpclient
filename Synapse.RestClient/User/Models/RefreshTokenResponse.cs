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
        [JsonProperty("expires_at")]
        [JsonConverter(typeof(StringUnixDateTimeConverter))]
        public DateTimeOffset ExpiresAt { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("oauth_key")]
        public string OAuthKey { get; set; }

        [JsonProperty("refresh_expires_in")]
        public string RefreshExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}