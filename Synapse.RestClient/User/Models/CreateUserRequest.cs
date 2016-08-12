using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Synapse.RestClient.User
{
    public class CreateUserRequest
    {
        public CreateUserRequestLogin[] Logins { get; set; }

        public string[] PhoneNumbers { get; set; }

        public string[] LegalNames { get; set; }

        public CreateUserRequestExtra Extra { get; set; }
    }

    public class CreateUserRequestLogin
    {
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }

    public class CreateUserRequestExtra
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        public string SuppId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsBusiness { get; set; }

        public int CipTag { get; set; }
    }
}
