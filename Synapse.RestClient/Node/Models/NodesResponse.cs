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
        public string ErrorCode { get; set; }

        public string HttpCode { get; set; }

        public NodeResponse[] Nodes { get; set; }

        public bool Success { get; set; }
    }
}
