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
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter), SynapseNodeType.Unknown)]
        public SynapseNodeType Type => SynapseNodeType.WireUS;

        public AddWireUSNodeRequestInfo Info { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddWireUSNodeRequestExtra Extra { get; set;}
    }

    public class AddWireUSNodeRequestInfo
    {
        public string Nickname { get; set; }

        public string NameOnAccount { get; set; }

        public string RoutingNum { get; set; }

        public string AccountNum { get; set; }

        public string BankName { get; set; }

        public string Address { get; set; }

        public AddWireUSNodeRequestCorrespondingInfo CorrespondentInfo { get; set; }
    }

    public class AddWireUSNodeRequestCorrespondingInfo
    {
        public string RoutingNum { get; set; }

        public string BankName { get; set; }

        public string Address { get; set; }
    }

    public class AddWireUSNodeRequestExtra
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? GatewayRestricted { get; set; }
    }
}
