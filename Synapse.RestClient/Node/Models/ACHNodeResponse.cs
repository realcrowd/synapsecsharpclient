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
        public ACHNodeResponse[] Nodes { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class ACHNodeResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_links")]
        public ACHNodeResponseLinks Links { get; set; }

        [JsonProperty("allowed")]
        [JsonConverter(typeof(SynapseNodePermissionEnumConverter))]
        public SynapseNodePermission Allowed { get; set; }

        [JsonProperty("extra")]
        public ACHNodeResponseExtra Extra { get; set; }

        [JsonProperty("info")]
        public ACHNodeResponseInfo Info { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ACHNodeResponseLinks
    {
        [JsonProperty("self")]
        public ACHNodeResponseSelf Self { get; set; }
    }

    public class ACHNodeResponseSelf
    {
        [JsonProperty("href")]
        public string Href { get; set; }
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

        [JsonProperty("bank_long_name")]
        public string BankLongName { get; set; }

        [JsonProperty("balance")]
        public ACHNodeResponseBalance Balance { get; set; }

        [JsonProperty("class")]
        [JsonConverter(typeof(SynapseNodeClassEnumConverter))]
        public SynapseNodeClass Class { get; set; }

        [JsonProperty("name_on_account")]
        public string NameOnAccount { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("routing_num")]
        public string RoutingNum { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTypeEnumConverter))]
        public SynapseNodeType Type { get; set; }
    }

    public class ACHNodeResponseBalance
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
