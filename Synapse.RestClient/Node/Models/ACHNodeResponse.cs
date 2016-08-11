using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public class ACHNodeResponse : NodeResponse
    {
        public ACHNodeResponseInfo Info { get; set; }
    }

    public class ACHNodeResponseExtra
    {
        public string SuppId { get; set; }
    }

    public class ACHNodeResponseInfo
    {
        public string AccountNum { get; set; }

        public string BankName { get; set; }

        public string BankLongName { get; set; }

        public ACHNodeResponseBalance Balance { get; set; }

        [JsonConverter(typeof(SynapseACHNodeClassEnumConverter), SynapseACHNodeClass.Unknown)]
        public SynapseACHNodeClass Class { get; set; }

        public string NameOnAccount { get; set; }

        public string Nickname { get; set; }

        public string RoutingNum { get; set; }

        [JsonConverter(typeof(SynapseACHNodeTypeEnumConverter), SynapseACHNodeType.Unknown)]
        public SynapseACHNodeType Type { get; set; }
    }

    public class ACHNodeResponseBalance
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
