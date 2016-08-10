using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class WireUSNodeResponse : NodeResponse
    {
        [JsonProperty("info")]
        public WireUSNodeResponseInfo Info { get; set; }
    }

    public class WireUSNodeResponseInfo
    {
        [JsonProperty("account_num")]
        public string AccountNum { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_long_name")]
        public string BankLongName { get; set; }

        [JsonProperty("correspondent_info")]
        public WireUSNodeResponseCorrespondentInfo CorrespondentInfo { get; set; }

        [JsonProperty("name_on_account")]
        public string NameOnAccount { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }
    }

    public class WireUSNodeResponseCorrespondentInfo
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }
    }
}
