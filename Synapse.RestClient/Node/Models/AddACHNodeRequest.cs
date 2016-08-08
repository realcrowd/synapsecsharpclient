using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class AddACHNodeRequest
    {
        [JsonProperty("type")]
        public string Type { get; } = "ACH-US";

        [JsonProperty("info")]
        public AddACHNodeRequestInfo Info { get; set; }

        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public AddACHNodeRequestExtra Extra { get; set;}
    }

    public class AddACHNodeRequestInfo
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("account_num")]
        public string AccountNum { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter))]
        public SynapseNodeType Type { get; set; }

        [JsonProperty("class")]
        [JsonConverter(typeof(SynapseNodeClassEnumConverter))]
        public SynapseNodeClass Class { get; set; }
    }

    public class AddACHNodeRequestExtra
    {
        [JsonProperty("supp_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty("gateway_restricted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? GatewayRestricted { get; set; }
    }
}
