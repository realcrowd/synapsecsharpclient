using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class VerifyNodeRequest
    {
        [JsonProperty("micro")]
        public decimal[] MicroDeposits { get; set; }
    }
}
