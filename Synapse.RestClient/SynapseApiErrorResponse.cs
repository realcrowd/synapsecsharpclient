using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    public class SynapseApiErrorResponse
    {
        [JsonProperty("error")]
        public Dictionary<string, string> Error { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("http_code")]
        public string HttpCode { get; set; }

        // Used for 2FA
        [JsonProperty("phone_numbers", NullValueHandling = NullValueHandling.Ignore)]
        public string[] PhoneNumbers { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
