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
        public AddTransactionRequestTo To { get; set; }

        public AddTransactionRequestAmount Amount { get; set; }

        public AddTransactionRequestExtra Extra { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestFee[] Fees { get; set; }
    }

    public class AddTransactionRequestTo
    {
        [JsonConverter(typeof(SynapseNodeTransactionTypeEnumConverter))]
        public SynapseNodeTransactionType Type { get; set; }

        public string Id { get; set; }
    }

    public class AddTransactionRequestAmount
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";
    }

    public class AddTransactionRequestExtra
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SuppId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Webhook { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ProcessOn { get; set; }

        public string Ip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestExtraOther Other { get; set; }
    }

    public class AddTransactionRequestExtraOther
    {
        public string[] Attachments { get; set; }
    }

    public class AddTransactionRequestFee
    {
        public decimal Fee { get; set; }

        public string Node { get; set; }

        public AddTransactionRequestFeeTo To { get; set; }
    }

    public class AddTransactionRequestFeeTo
    {
        public string Id { get; set; }
    }
}
