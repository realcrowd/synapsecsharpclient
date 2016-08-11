using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class TransactionResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_links")]
        public TransactionResponseLinks Links { get; set; }

        public TransactionResponseAmount Amount { get; set; }

        public TransactionResponseClient Client { get; set; }

        public TransactionResponseExtra Extra { get; set; }

        public TransactionResponseFee[] Fees { get; set; }

        public TransactionResponseNode From { get; set; }

        public TransactionResponseNode To { get; set; }

        public TransactionResponseStatusEntry RecentStatus { get; set; }

        public TransactionResponseStatusEntry[] Timeline { get; set; }
    }

    public class TransactionResponseLinks
    {
        public TransactionResponseLinksSelf Self { get; set; }
    }

    public class TransactionResponseLinksSelf
    {
        public string Href { get; set; }
    }

    public class TransactionResponseAmount
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";
    }

    public class TransactionResponseClient
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TransactionResponseExtra
    {
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? CreatedOn { get; set; }

        public string Ip { get; set; }

        [JsonProperty("latlon")]
        public string LatLon { get; set; }

        public string Note { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestExtraOther Other { get; set; }

        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? ProcessOn { get; set; }

        public string SuppId { get; set; }

        public string Webhook { get; set; }
    }

    public class TransactionResponseExtraOther
    {
        public string[] Attachments { get; set; }
    }

    public class TransactionResponseFee
    {
        public decimal Fee { get; set; }

        public string Note { get; set; }

        public TransactionResponseFeeTo To { get; set; }
    }

    public class TransactionResponseFeeTo
    {
        public string Id { get; set; }
    }

    public class TransactionResponseNode
    {
        [JsonConverter(typeof(SynapseNodeTransactionTypeEnumConverter))]
        public SynapseNodeTransactionType Type { get; set; }

        public string Id { get; set; }

        public string Nickname { get; set; }

        public TransactionResponseUser User { get; set; }
    }

    public class TransactionResponseUser
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string[] LegalNames { get; set; }
    }

    public class TransactionResponseStatusEntry
    {
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? Date { get; set; }

        public string Note { get; set; }

        [JsonProperty("status")]
        public string StatusDescription { get; set; }

        [JsonProperty("status_id")]
        [JsonConverter(typeof(SynapseTransactionStatusCodeEnumConverter))]
        public SynapseTransactionStatusCode Status { get; set; }
    }
}
