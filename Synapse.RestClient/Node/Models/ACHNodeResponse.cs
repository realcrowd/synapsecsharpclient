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
        [JsonProperty("info")]
        public ACHNodeResponseInfo Info { get; set; }
    }

    public class ACHNodeResponseExtra
    {
        [JsonProperty("supp_id")]
        public string SuppId { get; set; }
    }

    public class ACHNodeResponseInfo
    {
        [JsonProperty("account_num")]
        public string AccountNum { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_long_name")]
        public string BankLongName { get; set; }

        [JsonProperty("balance")]
        public ACHNodeResponseBalance Balance { get; set; }

        [JsonProperty("class")]
        [JsonConverter(typeof(SynapseACHNodeClassEnumConverter))]
        public SynapseACHNodeClass Class { get; set; }

        [JsonProperty("name_on_account")]
        public string NameOnAccount { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseACHNodeTypeEnumConverter))]
        public SynapseACHNodeType Type { get; set; }
    }

    public class ACHNodeResponseBalance
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
