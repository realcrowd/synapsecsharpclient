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

        [JsonProperty("allowed")]
        [JsonConverter(typeof(SynapseNodePermissionEnumConverter))]
        public SynapseNodePermission Allowed { get; set; }

        [JsonProperty("extra")]
        public ACHNodeResponseExtra Extra { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter))]
        public SynapseNodeType Type { get; set; }
    }

    public class NodeResponseLinks
    {
        [JsonProperty("self")]
        public NodeResponseSelf Self { get; set; }
    }

    public class NodeResponseSelf
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
