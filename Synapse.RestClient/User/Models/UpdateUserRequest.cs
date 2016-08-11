using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public class UpdateUserRequest
    {
        public string RefreshToken { get; set; }

        public UpdateUserRequestUpdate Update { get; set; }
    }

    public class UpdateUserRequestUpdate
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LegalName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateUserRequestLogin Login { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public UpdateUserRequestRemoveLogin RemoveLogin { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RemovePhoneNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int CipTag { get; set; }
    }

    public class UpdateUserRequestLogin
    {
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }

    public class UpdateUserRequestRemoveLogin
    {
        public string Email { get; set; }
    }
}
