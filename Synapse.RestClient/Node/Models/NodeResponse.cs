using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    [JsonConverter(typeof(NodeResponseConverter))]
    public abstract class NodeResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_links")]
        public NodeResponseLinks Links { get; set; }

        [JsonConverter(typeof(SynapseNodePermissionEnumConverter), SynapseNodePermission.Unknown)]
        public SynapseNodePermission Allowed { get; set; }

        public ACHNodeResponseExtra Extra { get; set; }

        public bool IsActive { get; set; }

        [JsonConverter(typeof(SynapseNodeTypeEnumConverter), SynapseNodeType.Unknown)]
        public SynapseNodeType Type { get; set; }
    }

    public class NodeResponseLinks
    {
        public NodeResponseSelf Self { get; set; }
    }

    public class NodeResponseSelf
    {
        public string Href { get; set; }
    }
}
