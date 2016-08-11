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
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter), SynapseNodeType.Unknown)]
        public SynapseNodeType Type => SynapseNodeType.ACHUS;

        public AddACHNodeRequestInfo Info { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddACHNodeRequestExtra Extra { get; set;}
    }

    public class AddACHNodeRequestInfo
    {
        public string Nickname { get; set; }

        public string AccountNum { get; set; }

        public string RoutingNum { get; set; }

        [JsonConverter(typeof(SynapseACHNodeTypeEnumConverter), SynapseACHNodeType.Unknown)]
        public SynapseACHNodeType Type { get; set; }

        [JsonConverter(typeof(SynapseACHNodeClassEnumConverter), SynapseACHNodeClass.Unknown)]
        public SynapseACHNodeClass Class { get; set; }
    }

    public class AddACHNodeRequestExtra
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? GatewayRestricted { get; set; }
    }
}
