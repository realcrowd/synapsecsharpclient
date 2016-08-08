using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Transaction
{
    public class CommentOnTransactionResponse
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("http_code")]
        public string HttpCode { get; set; }

        [JsonProperty("trans")]
        public TransactionResponse Transaction { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
