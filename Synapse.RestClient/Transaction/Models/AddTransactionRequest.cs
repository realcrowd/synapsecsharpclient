using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class AddTransactionRequest
    {
        [JsonProperty("to")]
        public AddTransactionRequestTo To { get; set; }

        [JsonProperty("amount")]
        public AddTransactionRequestAmount Amount { get; set; }

        [JsonProperty("extra")]
        public AddTransactionRequestExtra Extra { get; set; }

        [JsonProperty("fees", NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestFee[] Fees { get; set; }
    }

    public class AddTransactionRequestTo
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTransactionTypeEnumConverter))]
        public SynapseNodeTransactionType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class AddTransactionRequestAmount
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "USD";
    }

    public class AddTransactionRequestExtra
    {
        [JsonProperty("supp_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty("webhook", NullValueHandling = NullValueHandling.Ignore)]
        public string Webhook { get; set; }

        [JsonProperty("process_on", NullValueHandling = NullValueHandling.Ignore)]
        public int ProcessOn { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestExtraOther Other { get; set; }
    }

    public class AddTransactionRequestExtraOther
    {
        [JsonProperty("attachments")]
        public string[] Attachments { get; set; }
    }

    public class AddTransactionRequestFee
    {
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("node")]
        public string Node { get; set; }

        [JsonProperty("to")]
        public AddTransactionRequestFeeTo To { get; set; }
    }

    public class AddTransactionRequestFeeTo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
