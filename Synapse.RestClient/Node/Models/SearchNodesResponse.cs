using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class SearchNodesResponse
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("http_code")]
        public string HttpCode { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("page_count")]
        public int PageCount { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("nodes")]
        public ACHNodeResponse[] Nodes { get; set; }

        [JsonProperty("node_count")]
        public int NodeCount { get; set; }
    }
}
