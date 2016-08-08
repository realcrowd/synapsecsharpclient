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
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("update")]
        public UpdateUserRequestUpdate Update { get; set; }
    }

    public class UpdateUserRequestUpdate
    {
        [JsonProperty("legal_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LegalName { get; set; }

        [JsonProperty("login", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateUserRequestLogin Login { get; set; }

        [JsonProperty("remove_login", NullValueHandling = NullValueHandling.Ignore)]
        public UpdateUserRequestRemoveLogin RemoveLogin { get; set; }

        [JsonProperty("phone_number", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("remove_phone_number", NullValueHandling = NullValueHandling.Ignore)]
        public string RemovePhoneNumber { get; set; }

        [JsonProperty("cip_tag", NullValueHandling = NullValueHandling.Ignore)]
        public int CipTag { get; set; }
    }

    public class UpdateUserRequestLogin
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("read_only", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }

    public class UpdateUserRequestRemoveLogin
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
