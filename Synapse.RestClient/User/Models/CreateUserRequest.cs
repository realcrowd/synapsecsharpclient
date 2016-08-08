using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class CreateUserRequest
    {
        [JsonProperty("logins")]
        public CreateUserRequestLogin[] Logins { get; set; }

        [JsonProperty("phone_numbers")]
        public string[] PhoneNumbers { get; set; }

        [JsonProperty("legal_names")]
        public string[] LegalNames { get; set; }

        [JsonProperty("extra")]
        public CreateUserRequestExtra Extra { get; set; }
    }

    public class CreateUserRequestLogin
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("read_only", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }

    public class CreateUserRequestExtra
    {
        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty("supp_id")]
        public string SuppId { get; set; }

        [JsonProperty("is_business", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBusiness { get; set; }

        [JsonProperty("cip_tag")]
        public int CipTag { get; set; }
    }
}
