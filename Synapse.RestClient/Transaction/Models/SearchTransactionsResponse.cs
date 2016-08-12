using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class SearchTransactionsResponse
    {
        public string ErrorCode { get; set; }

        public string HttpCode { get; set; }

        public int Page { get; set; }

        public int PageCount { get; set; }

        public bool Success { get; set; }

        [JsonProperty("trans")]
        public TransactionResponse[] Transactions { get; set; }

        [JsonProperty("trans_count")]
        public int TransactionCount { get; set; }
    }
}
