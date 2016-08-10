using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class AddWireUSNodeRequest
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter))]
        public SynapseNodeType Type => SynapseNodeType.WireUS;

        [JsonProperty("info")]
        public AddWireUSNodeRequestInfo Info { get; set; }

        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public AddWireUSNodeRequestExtra Extra { get; set;}
    }

    public class AddWireUSNodeRequestInfo
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("name_on_account")]
        public string NameOnAccount { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }

        [JsonProperty("account_num")]
        public string AccountNum { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("correspondent_info")]
        public AddWireUSNodeRequestCorrespondingInfo CorrespondentInfo { get; set; }
    }

    public class AddWireUSNodeRequestCorrespondingInfo
    {
        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }

    public class AddWireUSNodeRequestExtra
    {
        [JsonProperty("supp_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty("gateway_restricted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? GatewayRestricted { get; set; }
    }
}
