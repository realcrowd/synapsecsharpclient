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

        [JsonProperty("amount")]
        public TransactionResponseAmount Amount { get; set; }

        [JsonProperty("client")]
        public TransactionResponseClient Client { get; set; }

        [JsonProperty("extra")]
        public TransactionResponseExtra Extra { get; set; }

        [JsonProperty("fees")]
        public TransactionResponseFee[] Fees { get; set; }

        [JsonProperty("from")]
        public TransactionResponseNode From { get; set; }

        [JsonProperty("to")]
        public TransactionResponseNode To { get; set; }

        [JsonProperty("recent_status")]
        public TransactionResponseStatusEntry RecentStatus { get; set; }

        [JsonProperty("timeline")]
        public TransactionResponseStatusEntry[] Timeline { get; set; }
    }

    public class TransactionResponseLinks
    {
        [JsonProperty("self")]
        public TransactionResponseLinksSelf Self { get; set; }
    }

    public class TransactionResponseLinksSelf
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class TransactionResponseAmount
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "USD";
    }

    public class TransactionResponseClient
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TransactionResponseExtra
    {
        [JsonProperty("created_on")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? CreatedOn { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("latlon")]
        public string LatLon { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
        public AddTransactionRequestExtraOther Other { get; set; }

        [JsonProperty("process_on")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? ProcessOn { get; set; }

        [JsonProperty("supp_id")]
        public string SuppId { get; set; }

        [JsonProperty("webhook")]
        public string Webhook { get; set; }
    }

    public class TransactionResponseExtraOther
    {
        [JsonProperty("attachments")]
        public string[] Attachments { get; set; }
    }

    public class TransactionResponseFee
    {
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("to")]
        public TransactionResponseFeeTo To { get; set; }
    }

    public class TransactionResponseFeeTo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class TransactionResponseNode
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(SynapseNodeTransactionTypeEnumConverter))]
        public SynapseNodeTransactionType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("user")]
        public TransactionResponseUser User { get; set; }
    }

    public class TransactionResponseUser
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("legal_names")]
        public string[] LegalNames { get; set; }
    }

    public class TransactionResponseStatusEntry
    {
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
        public DateTimeOffset? Date { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("status")]
        public string StatusDescription { get; set; }

        [JsonProperty("status_id")]
        [JsonConverter(typeof(SynapseTransactionStatusCodeEnumConverter))]
        public SynapseTransactionStatusCode Status { get; set; }
    }
}
