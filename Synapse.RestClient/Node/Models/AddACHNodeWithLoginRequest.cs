using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class AddACHNodeWithLoginRequest
    {
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter), SynapseNodeType.Unknown)]
        public SynapseNodeType Type => SynapseNodeType.ACHUS;

        public AddACHNodeWithLoginRequestInfo Info { get; set; }

        /* Can't seem to be able to set a Supp Id through this
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public AddACHNodeRequestExtra Extra { get; set;}
        */
    }

    public class AddACHNodeWithLoginRequestInfo
    {
        public string BankId { get; set; }

        [JsonProperty("bank_pw")]
        public string BankPassword { get; set; }

        public string BankName { get; set; }
    }
}
