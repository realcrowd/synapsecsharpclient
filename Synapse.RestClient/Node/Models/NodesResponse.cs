using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class NodesResponse
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("http_code")]
        public string HttpCode { get; set; }

        [JsonProperty("nodes")]
        public NodeResponse[] Nodes { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
